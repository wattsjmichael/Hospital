using Microsoft.EntityFrameworkCore;

namespace Hospital.Models
{
  public class HospitalContext : DbContext
  {
    public virtual DbSet<Doctor> Doctors { get; set;}
    public virtual DbSet<Patient> Patients {get; set;}

    public DbSet<DoctorPatient> DoctorPatient {get; set;}

    public HospitalContext(DbContextOptions options) : base(options) { }
  }
}