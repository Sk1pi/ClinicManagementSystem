using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Dto;

public class PatientDto
{
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}