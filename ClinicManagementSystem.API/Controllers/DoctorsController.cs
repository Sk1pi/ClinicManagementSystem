using ClinicManagementSystem.Db;
using ClinicManagementSystem.Dto;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly ClinicContext _context;
    public DoctorsController(ClinicContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors() => 
        await _context.Doctors.Include(d => d.Department).ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Doctor>> GetDoctor(Guid id)
    {
        var item = await _context.Doctors.Include(d => d.Department).FirstOrDefaultAsync(d => d.Id == id);
        return item == null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<Doctor>> PostDoctor(DoctorDto dto)
    {
        var item = new Doctor 
        { 
            FirstName = dto.FirstName, 
            LastName = dto.LastName, 
            Specialization = dto.Specialization, 
            ExperienceYears = dto.ExperienceYears, 
            DepartmentId = dto.DepartmentId 
        };
        _context.Doctors.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDoctor), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDoctor(Guid id, DoctorDto dto)
    {
        var item = await _context.Doctors.FindAsync(id);
        if (item == null) return NotFound();

        item.FirstName = dto.FirstName;
        item.LastName = dto.LastName;
        item.Specialization = dto.Specialization;
        item.ExperienceYears = dto.ExperienceYears;
        item.DepartmentId = dto.DepartmentId;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor(Guid id)
    {
        var item = await _context.Doctors.FindAsync(id);
        if (item == null) return NotFound();
        _context.Doctors.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}