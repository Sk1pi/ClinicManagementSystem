using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models;

public class Department
{
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    public string Location { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}