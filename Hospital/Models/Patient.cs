using System.Collections.Generic;

namespace Hospital.Models
{
  public class Patient
  {
    public Patient()
    {
      this.Doctors = new HashSet<DoctorPatient>();

    }
    public int PatientId {get; set; }
    public string PatientName {get; set;}
    public string PatientBirthDate {get; set; }
    
    public ICollection<DoctorPatient> Doctors { get; }

  }

}