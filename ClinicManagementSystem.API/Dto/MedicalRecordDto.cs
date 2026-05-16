using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Dto;

public class MedicalRecordDto
{
    [Required] public Guid PatientId { get; set; }
    [Required] public DateTime CreationDate { get; set; }
}