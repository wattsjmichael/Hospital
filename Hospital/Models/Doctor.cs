using System.Collections.Generic;


namespace Hospital.Models
{
  public class Doctor
  {
    public Doctor()
    {
      this.Patients = new HashSet<DoctorPatient>();
      this.Specialties= new HashSet<DoctorSpecialty>();
    }
    public int DoctorId { get; set; }
    public string DoctorName { get; set; }
    public string Specialty { get; set; }
    public virtual ICollection<DoctorPatient> Patients { get; set; }
    public virtual ICollection<DoctorSpecialty> Specialties { get; set; }
  }
}