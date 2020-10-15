using System.Collections.Generic;

namespace Hospital.Models
{
  public class Specialty
  {
    public Specialty()
    {
      this.Doctors = new HashSet<DoctorSpecialty>();

    }
    public int SpecialtyId {get; set; }
    public string SpecialtyName {get; set;}
    
    public ICollection<DoctorSpecialty> Doctors { get; }

  }

}