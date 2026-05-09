using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models;

public class Patient
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Ім'я є обов'язковим")]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}