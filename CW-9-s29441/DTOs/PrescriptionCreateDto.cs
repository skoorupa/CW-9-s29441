using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using CW_9_s29441.Models;

namespace CW_9_s29441.DTOs;

public class PrescriptionCreateDto
{
    [Required] 
    public PatientCreateDto Patient { get; set; } = null!;
    
    [Required]
    public DoctorCreateDto Doctor { get; set; } = null!;
    
    [Required]
    public ICollection<PrescriptionMedicamentDtoMedicament> Medicaments { get; set; } = null!;
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
}

public class PatientCreateDto
{
    [Required]
    public int IdPatient { get; set; }
    
    [Required]
    [MaxLength(100)] 
    public string FirstName { get; set; } = null!;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    
    [Required]
    public DateTime Birthdate { get; set; }
}

public class DoctorCreateDto
{
    [Required]
    public int IdDoctor { get; set; }

    [Required]
    [MaxLength(100)] 
    public string FirstName { get; set; } = null!;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    
    [Required] [EmailAddress] [MaxLength(100)] 
    public string Email { get; set; } = null!;
}

public class PrescriptionMedicamentDtoMedicament
{
    [Required]
    public int IdMedicament { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    
    [Required]
    [MaxLength(100)]
    public string Description { get; set; } = null!;
    
    [Required]
    [MaxLength(100)]
    public string Type { get; set; } = null!;
    
    public int? Dose { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Details { get; set; } = null!;
}