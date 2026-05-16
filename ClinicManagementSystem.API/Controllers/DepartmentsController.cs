using ClinicManagementSystem.Db;
using ClinicManagementSystem.Dto;
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
    public async Task<ActionResult<Department>> PostDepartment(DepartmentDto dto)
    {
        var item = new Department { Name = dto.Name, Location = dto.Location };
        _context.Departments.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDepartment), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDepartment(Guid id, DepartmentDto dto)
    {
        var item = await _context.Departments.FindAsync(id);
        if (item == null) return NotFound();
        
        item.Name = dto.Name;
        item.Location = dto.Location;
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