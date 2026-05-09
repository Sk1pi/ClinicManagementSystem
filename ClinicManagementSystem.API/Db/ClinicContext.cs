using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Db;

public class ClinicContext : DbContext
{
    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<Doctor> Doctors { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Appointment> Appointments { get; set; }
    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }
    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public ClinicContext(DbContextOptions<ClinicContext> options) : base(options)
    {
    }
}