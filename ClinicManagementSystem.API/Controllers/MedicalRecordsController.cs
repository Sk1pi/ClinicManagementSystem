using ClinicManagementSystem.Db;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicalRecordsController : ControllerBase
{
    private readonly ClinicContext _context;
    public MedicalRecordsController(ClinicContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetMedicalRecords() => 
        await _context.MedicalRecords.Include(m => m.Patient).Include(m => m.Diagnoses).ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<MedicalRecord>> GetMedicalRecord(Guid id)
    {
        var item = await _context.MedicalRecords.Include(m => m.Patient).Include(m => m.Diagnoses).FirstOrDefaultAsync(m => m.Id == id);
        return item == null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<MedicalRecord>> PostMedicalRecord(MedicalRecord item)
    {
        _context.MedicalRecords.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMedicalRecord), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMedicalRecord(Guid id, MedicalRecord item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedicalRecord(Guid id)
    {
        var item = await _context.MedicalRecords.FindAsync(id);
        if (item == null) return NotFound();
        _context.MedicalRecords.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}