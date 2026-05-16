using ClinicManagementSystem.Controllers;
using ClinicManagementSystem.Db;
using ClinicManagementSystem.Dto;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Tests;

public class DepartmentsControllerTests
{
    private async Task<ClinicContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<ClinicContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            
        var databaseContext = new ClinicContext(options);
        await databaseContext.Database.EnsureCreatedAsync();
        return databaseContext;
    }

    [Fact]
    public async Task GetDepartments_ReturnsAllDepartments()
    {
        // Arrange
        var context = await GetDatabaseContext();
        context.Departments.Add(new Department { Id = Guid.NewGuid(), Name = "Хірургія", Location = "1 поверх" });
        context.Departments.Add(new Department { Id = Guid.NewGuid(), Name = "Терапія", Location = "2 поверх" });
        await context.SaveChangesAsync();
        
        var controller = new DepartmentsController(context);

        // Act
        var result = await controller.GetDepartments();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<Department>>>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Department>>(actionResult.Value);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public async Task GetDepartment_ReturnsNotFound_WhenIdIsInvalid()
    {
        // Arrange
        var context = await GetDatabaseContext();
        var controller = new DepartmentsController(context);
        var invalidId = Guid.NewGuid();

        // Act
        var result = await controller.GetDepartment(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostDepartment_AddsNewDepartmentAndReturnsCreated()
    {
        // Arrange
        var context = await GetDatabaseContext();
        var controller = new DepartmentsController(context);
        var newDto = new DepartmentDto { Name = "Неврологія", Location = "3 поверх" };

        // Act
        var result = await controller.PostDepartment(newDto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var createdItem = Assert.IsType<Department>(actionResult.Value);
        Assert.Equal("Неврологія", createdItem.Name);
        Assert.Equal(1, await context.Departments.CountAsync());
    }

    [Fact]
    public async Task PutDepartment_UpdatesExistingDepartment()
    {
        // Arrange
        var context = await GetDatabaseContext();
        var id = Guid.NewGuid();
        context.Departments.Add(new Department { Id = id, Name = "Стара назва", Location = "Стара локація" });
        await context.SaveChangesAsync();
        
        var controller = new DepartmentsController(context);
        var updateDto = new DepartmentDto { Name = "Нова назва", Location = "Нова локація" };

        // Act
        var result = await controller.PutDepartment(id, updateDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
        var updatedItem = await context.Departments.FindAsync(id);
        Assert.Equal("Нова назва", updatedItem.Name);
    }

    [Fact]
    public async Task DeleteDepartment_RemovesDepartmentFromDatabase()
    {
        // Arrange
        var context = await GetDatabaseContext();
        var id = Guid.NewGuid();
        context.Departments.Add(new Department { Id = id, Name = "Тест", Location = "Тест" });
        await context.SaveChangesAsync();
        
        var controller = new DepartmentsController(context);

        // Act
        var result = await controller.DeleteDepartment(id);

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.Equal(0, await context.Departments.CountAsync());
    }
}