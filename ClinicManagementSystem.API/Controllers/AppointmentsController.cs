using ClinicManagementSystem.Db;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly ClinicContext _context;
    public AppointmentsController(ClinicContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments() => 
        await _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Appointment>> GetAppointment(Guid id)
    {
        var item = await _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).FirstOrDefaultAsync(a => a.Id == id);
        return item == null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<Appointment>> PostAppointment(Appointment item)
    {
        _context.Appointments.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAppointment), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAppointment(Guid id, Appointment item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(Guid id)
    {
        var item = await _context.Appointments.FindAsync(id);
        if (item == null) return NotFound();
        _context.Appointments.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}