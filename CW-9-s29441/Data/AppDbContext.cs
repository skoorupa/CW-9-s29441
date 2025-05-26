using CW_9_s29441.Models;
using Microsoft.EntityFrameworkCore;

namespace CW_9_s29441.Data;

public class AppDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var doctors = new List<Doctor>
        {
            new (){IdDoctor = 1, FirstName = "Adam", LastName = "Skorupski", Email = "s29441@pjwstk.edu.pl"},
            new (){IdDoctor = 2, FirstName = "Tomasz", LastName = "Tomaszowski", Email = "tomasz@tomaszowski.edu.pl"},
            new (){IdDoctor = 3, FirstName = "Anna", LastName = "Annowska", Email = "anna@annowska.pl"},
        };

        var patients = new List<Patient>
        {
            new (){IdPatient = 1, FirstName = "Magdalena", LastName = "Lena", Birthdate = new DateTime(2000,1,1)},
            new (){IdPatient = 2, FirstName = "Katarzyna", LastName = "Kata", Birthdate = new DateTime(2000,2,2)},
            new (){IdPatient = 3, FirstName = "Aleksandra", LastName = "Mandra", Birthdate = new DateTime(2000,3,3)}
        };

        var medicaments = new List<Medicament>
        {
            new (){IdMedicament = 1, Name = "APAP", Description = "Paracetamol", Type = "przeciwbólowy"},
            new (){IdMedicament = 2, Name = "Ibuprom", Description = "Ibuprofen", Type = "przeciwbólowy"},
            new (){IdMedicament = 3, Name = "Rutinoscorbin", Description = "Witamina C", Type = "odpornościowy"},
        };

        var prescriptions = new List<Prescription>
        {
            new (){IdPrescription = 1, Date = DateTime.Today, DueDate = DateTime.Today.AddDays(14), IdPatient = 1, IdDoctor = 1},
            new (){IdPrescription = 2, Date = DateTime.Today.AddDays(-1), DueDate = DateTime.Today.AddDays(7), IdPatient = 1, IdDoctor = 2},
            new (){IdPrescription = 3, Date = DateTime.Today.AddDays(-2), DueDate = DateTime.Today.AddDays(7), IdPatient = 2, IdDoctor = 2},
            new (){IdPrescription = 4, Date = DateTime.Today.AddDays(-3), DueDate = DateTime.Today.AddDays(7), IdPatient = 3, IdDoctor = 3},
        };

        var prescriptionMedicaments = new List<PrescriptionMedicament>
        {
            new (){IdMedicament = 1, IdPrescription = 1, Dose = 5, Details = "codziennie wieczorem"},
            new (){IdMedicament = 2, IdPrescription = 1, Details = "raz na tydzień"},
            new (){IdMedicament = 2, IdPrescription = 2, Dose = 1, Details = "co 2 dni"},
            new (){IdMedicament = 3, IdPrescription = 3, Dose = 2, Details = "codziennie rano"},
        };
        
        modelBuilder.Entity<Doctor>().HasData(doctors);
        modelBuilder.Entity<Patient>().HasData(patients);
        modelBuilder.Entity<Medicament>().HasData(medicaments);
        modelBuilder.Entity<Prescription>().HasData(prescriptions);
        modelBuilder.Entity<PrescriptionMedicament>().HasData(prescriptionMedicaments);
    }
}