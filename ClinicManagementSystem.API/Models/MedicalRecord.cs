namespace ClinicManagementSystem.Models;

public class MedicalRecord
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public DateTime CreationDate { get; set; }

    public virtual Patient Patient { get; set; }
    
    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
}