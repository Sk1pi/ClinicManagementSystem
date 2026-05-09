using ClinicManagementSystem.Db;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly ClinicContext _context;
    public DepartmentsController(ClinicContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments() => 
        await _context.Departments.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetDepartment(Guid id)
    {
        var item = await _context.Departments.FindAsync(id);
        return item == null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<Department>> PostDepartment(Department item)
    {
        _context.Departments.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDepartment), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDepartment(Guid id, Department item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(Guid id)
    {
        var item = await _context.Departments.FindAsync(id);
        if (item == null) return NotFound();
        _context.Departments.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}