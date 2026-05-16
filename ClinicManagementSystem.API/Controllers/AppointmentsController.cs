using ClinicManagementSystem.Db;
using ClinicManagementSystem.Dto;
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
    public async Task<ActionResult<Appointment>> PostAppointment(AppointmentDto dto)
    {
        var item = new Appointment 
        { 
            PatientId = dto.PatientId, 
            DoctorId = dto.DoctorId, 
            AppointmentDate = dto.AppointmentDate, 
            Status = (AppointmentStatus)dto.Status, 
            ReasonForVisit = dto.ReasonForVisit 
        };
        _context.Appointments.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAppointment), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAppointment(Guid id, AppointmentDto dto)
    {
        var item = await _context.Appointments.FindAsync(id);
        if (item == null) return NotFound();

        item.PatientId = dto.PatientId;
        item.DoctorId = dto.DoctorId;
        item.AppointmentDate = dto.AppointmentDate;
        item.Status = (AppointmentStatus)dto.Status;
        item.ReasonForVisit = dto.ReasonForVisit;
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