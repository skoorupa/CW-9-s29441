using CW_9_s29441.Data;
using CW_9_s29441.DTOs;
using CW_9_s29441.Exceptions;
using CW_9_s29441.Models;
using Microsoft.EntityFrameworkCore;

namespace CW_9_s29441.Services;

public interface IDbService
{
    public Task<PrescriptionCreateDto> CreatePrescriptionAsync(PrescriptionCreateDto prescriptionData);
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<PrescriptionCreateDto> CreatePrescriptionAsync(PrescriptionCreateDto prescriptionData)
    {
        // walidacja
        if (prescriptionData.DueDate < prescriptionData.Date)
            throw new IncorrectDateException("Due date must be greater than or equal to Date");
        
        if (prescriptionData.Medicaments.Count > 10)
            throw new ArgumentOutOfRangeException(nameof(prescriptionData.Medicaments), "Medicaments count must be less than or equal to 10");

        foreach (var medicamentRequest in prescriptionData.Medicaments)
        {
            var medicamentDto = await data.Medicaments.FirstOrDefaultAsync(m => m.IdMedicament == medicamentRequest.IdMedicament);
            if (medicamentDto == null)
                throw new NotFoundException($"Medicament with id {medicamentRequest.IdMedicament} not found");
        }
        
        var doctor = await data.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == prescriptionData.Doctor.IdDoctor);
        if (doctor is null)
            throw new NotFoundException($"Doctor not found");
        
        // koniec walidacji
        
        var patient = await data.Patients.FirstOrDefaultAsync(p => p.IdPatient == prescriptionData.Patient.IdPatient);
        if (patient is null)
        {
            var patientData = prescriptionData.Patient;
            await data.Patients.AddAsync(new Patient()
            {
                FirstName = patientData.FirstName,
                LastName = patientData.LastName,
                Birthdate = patientData.Birthdate
            });
            await data.SaveChangesAsync(); 
            patient = await data.Patients.FirstOrDefaultAsync(p => p.IdPatient == prescriptionData.Patient.IdPatient);
            if (patient is null)
                throw new Exception("Cannot add and find patient");
        }

        var prescription = await data.Prescriptions.AddAsync(new Prescription()
        {
            Date = prescriptionData.Date,
            DueDate = prescriptionData.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = doctor.IdDoctor
        });
        await data.SaveChangesAsync();
        var prescriptionId = prescription.Entity.IdPrescription;
        
        foreach (var medicamentDto in prescriptionData.Medicaments)
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
        return prescriptionData;
    }
}