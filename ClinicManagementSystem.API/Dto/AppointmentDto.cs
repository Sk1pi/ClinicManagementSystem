using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Dto;

public class AppointmentDto
{
    [Required] public Guid PatientId { get; set; }
    [Required] public Guid DoctorId { get; set; }
    [Required] public DateTime AppointmentDate { get; set; }
    public int Status { get; set; }
    public string ReasonForVisit { get; set; }
}