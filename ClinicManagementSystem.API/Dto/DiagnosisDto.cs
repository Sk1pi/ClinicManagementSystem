using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Dto;

public class DiagnosisDto
{
    [Required] public Guid MedicalRecordId { get; set; }
    [Required] public Guid DoctorId { get; set; }
    [Required] public DateTime DateDiagnosed { get; set; }
    [Required] public string DiseaseName { get; set; }
    public string Prescription { get; set; }
}