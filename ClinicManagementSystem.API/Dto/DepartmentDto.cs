using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Dto;

public class DepartmentDto
{
    [Required] public string Name { get; set; }
    public string Location { get; set; }
}