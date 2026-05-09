namespace ClinicManagementSystem.Models;

public class Doctor
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Specialization { get; set; }
    public int ExperienceYears { get; set; }
    
    public Guid DepartmentId { get; set; }
    public virtual Department Department { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}