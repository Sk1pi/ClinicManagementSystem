using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Dto;

public class DoctorDto
{
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string Specialization { get; set; }
    public int ExperienceYears { get; set; }
    [Required] public Guid DepartmentId { get; set; }
}