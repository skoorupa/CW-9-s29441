namespace CW_9_s29441.DTOs;

public class PatientGetDto
{
    public int IdPatient { get; set; }
    
    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    public DateTime Birthdate { get; set; }
    
    public ICollection<PatientGetDtoPrescription> Prescriptions { get; set; } = null!;
}

public class PatientGetDtoPrescription
{
    public int IdPrescription { get; set; }
    
    public DateTime Date { get; set; }
    
    public DateTime DueDate { get; set; }

    public ICollection<PatientGetDtoMedicament> Medicaments { get; set; } = null!;
    
    public PatientGetDtoDoctor Doctor { get; set; }
}

public class PatientGetDtoMedicament
{
    public int IdMedicament { get; set; }
    
    public string Name { get; set; } = null!;
    
    public int? Dose { get; set; } 
    
    public string Description { get; set; } = null!;
}

public class PatientGetDtoDoctor
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; } = null!;
}