using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models;

public enum AppointmentStatus
{
    Scheduled,
    Completed,
    Cancelled
}

public class Appointment
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    
    [Required]
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; }
    public string ReasonForVisit { get; set; }

    public virtual Patient? Patient { get; set; }
    public virtual Doctor? Doctor { get; set; }
}