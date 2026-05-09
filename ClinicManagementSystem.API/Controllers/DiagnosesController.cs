using ClinicManagementSystem.Db;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiagnosesController : ControllerBase
{
    private readonly ClinicContext _context;
    public DiagnosesController(ClinicContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Diagnosis>>> GetDiagnoses() => 
        await _context.Diagnoses.Include(d => d.Doctor).ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Diagnosis>> GetDiagnosis(Guid id)
    {
        var item = await _context.Diagnoses.Include(d => d.Doctor).FirstOrDefaultAsync(d => d.Id == id);
        return item == null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<Diagnosis>> PostDiagnosis(Diagnosis item)
    {
        _context.Diagnoses.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDiagnosis), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDiagnosis(Guid id, Diagnosis item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiagnosis(Guid id)
    {
        var item = await _context.Diagnoses.FindAsync(id);
        if (item == null) return NotFound();
        _context.Diagnoses.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}