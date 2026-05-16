using ClinicManagementSystem.Db;
using ClinicManagementSystem.Dto;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly ClinicContext _context;
    public PatientsController(ClinicContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatients() => 
        await _context.Patients.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> GetPatient(Guid id)
    {
        var item = await _context.Patients.FindAsync(id);
        return item == null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<Patient>> PostPatient(PatientDto dto)
    {
        var item = new Patient 
        { 
            FirstName = dto.FirstName, 
            LastName = dto.LastName, 
            DateOfBirth = dto.DateOfBirth, 
            PhoneNumber = dto.PhoneNumber, 
            Email = dto.Email 
        };
        _context.Patients.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPatient), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPatient(Guid id, PatientDto dto)
    {
        var item = await _context.Patients.FindAsync(id);
        if (item == null) return NotFound();

        item.FirstName = dto.FirstName;
        item.LastName = dto.LastName;
        item.DateOfBirth = dto.DateOfBirth;
        item.PhoneNumber = dto.PhoneNumber;
        item.Email = dto.Email;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(Guid id)
    {
        var item = await _context.Patients.FindAsync(id);
        if (item == null) return NotFound();
        _context.Patients.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}