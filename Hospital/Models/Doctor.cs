using System.Collections.Generic;


namespace Hospital.Models
{
  public class Doctor
  {
    public Doctor()
    {
      this.Patients = new HashSet<DoctorPatient>();
    }
    public int DoctorId { get; set; }
    public string DoctorName { get; set; }
    public string Specialty { get; set; }
    public virtual ICollection<DoctorPatient> Patients { get; set; }
  }
}