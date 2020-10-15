using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital.Models;

namespace Hospital.Controllers
{
  public class DoctorsController : Controller
  {
    private readonly HospitalContext _db;

    public DoctorsController(HospitalContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<Doctor> model = _db.Doctors.ToList();
      return View(model);
    }
    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Create(Doctor doctor)
    {
      _db.Doctors.Add(doctor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      Doctor ThisDoctor = _db.Doctors
      .Include(doctor => doctor.Patients)
      .ThenInclude(join => join.Patient)
      .Include(x => x.Specialties)
      .ThenInclude(join => join.Specialty)
      .FirstOrDefault(doctor => doctor.DoctorId == id);
      return View(ThisDoctor);
    }

    public ActionResult Edit(int id)
    {
      Doctor thisDoctor = _db.Doctors.FirstOrDefault(x => x.DoctorId == id);
      return View(thisDoctor);
    }

    [HttpPost]
    public ActionResult Edit(Doctor doctor)
    {
      _db.Entry(doctor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Doctor thisDoctor = _db.Doctors.FirstOrDefault(x => x.DoctorId == id);
      return View(thisDoctor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Doctor thisDoctor = _db.Doctors.FirstOrDefault(x => x.DoctorId == id);
      _db.Doctors.Remove(thisDoctor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddPatient(int id)
    {
      Doctor thisDoctor = _db.Doctors.FirstOrDefault(x => x.DoctorId == id);
      ViewBag.PatientId = new SelectList(_db.Patients, "PatientId", "PatientName"); //PatientName == Dropdown
      return View(thisDoctor);
    }
    
    [HttpPost]
    public ActionResult AddPatient(Doctor doctor, int PatientId)
    {
      if (PatientId != 0)
      {
        _db.DoctorPatient.Add(new DoctorPatient() { DoctorId = doctor.DoctorId, PatientId = PatientId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = doctor.DoctorId});
    }

    [HttpPost]
    public ActionResult RemovePatient(int DoctorPatientId)
    {
      DoctorPatient joinEntry = _db.DoctorPatient.FirstOrDefault(x => x.DoctorPatientId == DoctorPatientId);
      _db.DoctorPatient.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddSpecialty(int id)
    {
      Doctor thisDoctor = _db.Doctors.FirstOrDefault(x => x.DoctorId == id);
      ViewBag.SpecialtyId = new SelectList(_db.Specialties, "SpecialtyId", "SpecialtyName"); //PatientName == Dropdown
      return View(thisDoctor);
    }

    [HttpPost]
    public ActionResult AddSpecialty(Doctor doctor, int SpecialtyId)
    {
      if (SpecialtyId != 0)
      {
        _db.DoctorSpecialty.Add(new DoctorSpecialty() { DoctorId = doctor.DoctorId, SpecialtyId = SpecialtyId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = doctor.DoctorId});
    }

    [HttpPost]
    public ActionResult RemoveSpecialty(int DoctorSpecialtyId)
    {
      DoctorSpecialty joinEntry = _db.DoctorSpecialty.FirstOrDefault(x => x.DoctorSpecialtyId == DoctorSpecialtyId);
      _db.DoctorSpecialty.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }

}

