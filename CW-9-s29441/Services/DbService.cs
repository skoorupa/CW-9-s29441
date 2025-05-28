using CW_9_s29441.Data;
using CW_9_s29441.DTOs;
using CW_9_s29441.Exceptions;
using CW_9_s29441.Models;
using Microsoft.EntityFrameworkCore;

namespace CW_9_s29441.Services;

public interface IDbService
{
    public Task<PrescriptionCreateDto> CreatePrescriptionAsync(PrescriptionCreateDto prescriptionCreateDto);
    public Task<PatientGetDto> GetPatientDetailsAsync(int patientId);
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<PrescriptionCreateDto> CreatePrescriptionAsync(PrescriptionCreateDto prescriptionCreateDto)
    {
        // walidacja
        if (prescriptionCreateDto.DueDate < prescriptionCreateDto.Date)
            throw new IncorrectDateException("Due date must be greater than or equal to Date");
        
        if (prescriptionCreateDto.Medicaments.Count > 10)
            throw new ArgumentOutOfRangeException(nameof(prescriptionCreateDto.Medicaments), "Medicaments count must be less than or equal to 10");

        foreach (var medicamentRequest in prescriptionCreateDto.Medicaments)
        {
            var medicamentDto = await data.Medicaments.FirstOrDefaultAsync(m => m.IdMedicament == medicamentRequest.IdMedicament);
            if (medicamentDto == null)
                throw new NotFoundException($"Medicament with id {medicamentRequest.IdMedicament} not found");
        }
        
        var doctor = await data.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == prescriptionCreateDto.Doctor.IdDoctor);
        if (doctor is null)
            throw new NotFoundException("Doctor not found");
        
        // koniec walidacji
        
        prescriptionCreateDto.Doctor.FirstName = doctor.FirstName;
        prescriptionCreateDto.Doctor.LastName = doctor.LastName;
        prescriptionCreateDto.Doctor.Email = doctor.Email;
        
        var patient = await data.Patients.FirstOrDefaultAsync(p => p.IdPatient == prescriptionCreateDto.Patient.IdPatient);
        if (patient is null)
        {
            var patientData = prescriptionCreateDto.Patient;
            var newPatient = await data.Patients.AddAsync(new Patient()
            {
                FirstName = patientData.FirstName,
                LastName = patientData.LastName,
                Birthdate = patientData.Birthdate
            });
            await data.SaveChangesAsync(); 
            patient = await data.Patients.FirstOrDefaultAsync(p => p.IdPatient == newPatient.Entity.IdPatient);
            if (patient is null)
                throw new Exception("Cannot add and find patient");
            prescriptionCreateDto.Patient.IdPatient = newPatient.Entity.IdPatient;
        }
        prescriptionCreateDto.Patient.FirstName = patient.FirstName;
        prescriptionCreateDto.Patient.LastName = patient.LastName;
        prescriptionCreateDto.Patient.Birthdate = patient.Birthdate;

        var prescription = await data.Prescriptions.AddAsync(new Prescription()
        {
            Date = prescriptionCreateDto.Date,
            DueDate = prescriptionCreateDto.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = doctor.IdDoctor
        });
        await data.SaveChangesAsync();
        var prescriptionId = prescription.Entity.IdPrescription;
        
        foreach (var medicamentDto in prescriptionCreateDto.Medicaments)
        {
            await data.PrescriptionMedicaments.AddAsync(new PrescriptionMedicament()
            {
                IdMedicament = medicamentDto.IdMedicament,
                IdPrescription = prescriptionId,
                Dose = medicamentDto.Dose,
                Details = medicamentDto.Details
            });
        }
        await data.SaveChangesAsync();
        return prescriptionCreateDto;
    }

    public async Task<PatientGetDto> GetPatientDetailsAsync(int patientId)
    {
        var result = await data.Patients.Select(p => new PatientGetDto
        {
            IdPatient = p.IdPatient,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Birthdate = p.Birthdate,
            Prescriptions = p.Prescriptions.Select(pr => new PatientGetDtoPrescription
            {
                IdPrescription = pr.IdPrescription,
                Date = pr.Date,
                DueDate = pr.DueDate,
                Medicaments = pr.PrescriptionMedicaments.Select(pm => new PatientGetDtoMedicament
                {
                    IdMedicament = pm.IdMedicament,
                    Name = pm.Medicament.Name,
                    Dose = pm.Dose,
                    Description = pm.Medicament.Description
                }).ToList(),
                Doctor = new PatientGetDtoDoctor
                {
                    IdDoctor = pr.Doctor.IdDoctor,
                    FirstName = pr.Doctor.FirstName
                }
            }).OrderBy(pr => pr.DueDate).ToList()
        }).FirstOrDefaultAsync(p => p.IdPatient == patientId);
        return result ?? throw new NotFoundException($"Patient with id {patientId} not found");
    }
}