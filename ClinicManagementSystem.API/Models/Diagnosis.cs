using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models;

public class Diagnosis
{
    public Guid Id { get; set; }
    public Guid MedicalRecordId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime DateDiagnosed { get; set; }
    
    [Required]
    public string DiseaseName { get; set; }
    public string Prescription { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; }
    public virtual Doctor Doctor { get; set; }
}