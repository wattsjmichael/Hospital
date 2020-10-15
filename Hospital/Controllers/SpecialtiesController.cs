using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalList.Controllers
{
  public class SpecialtiesController : Controller
  {
    private readonly HospitalContext _db;

    public SpecialtiesController(HospitalContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List <Specialty> model = _db.Specialties.ToList();
      return View (model);
    }
    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Create(Specialty specialty)
    {
      _db.Specialties.Add(specialty);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Specialty thisSpecialty  = _db.Specialties
      .Include(specialty => specialty.Doctors)
      .ThenInclude(join => join.Doctor)
      .FirstOrDefault(x => x.SpecialtyId == id);
      return View(thisSpecialty);
    }

    public ActionResult Edit(int id)
    {
      Specialty thisSpecialty = _db.Specialties.FirstOrDefault(x=>x.SpecialtyId == id);
      return View(thisSpecialty);
    }

    [HttpPost]
    public ActionResult Edit(Specialty specialty)
    {
      _db.Entry(specialty).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      Specialty thisSpecialty = _db.Specialties.FirstOrDefault(x => x.SpecialtyId == id);
      return View(thisSpecialty);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Specialty thisSpecialty = _db.Specialties.FirstOrDefault(x => x.SpecialtyId ==id);
      _db.Specialties.Remove(thisSpecialty);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    public ActionResult AddDoctor(int id)
    {
      Specialty thisSpecialty = _db.Specialties.FirstOrDefault(x => x.SpecialtyId ==id);
      ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "DoctorName");
      return View(thisSpecialty);
    }
    [HttpPost]
    public ActionResult AddDoctor(Specialty specialty, int DoctorId)
    {
      if (DoctorId != 0)
      {
        _db.DoctorSpecialty.Add(new DoctorSpecialty() { DoctorId = DoctorId, SpecialtyId = specialty.SpecialtyId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult RemoveDoctor(int DoctorSpecialtyId)
    {
      DoctorSpecialty joinEntry = _db.DoctorSpecialty.FirstOrDefault(x => x.DoctorSpecialtyId == DoctorSpecialtyId);
      _db.DoctorSpecialty.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index"); // no view for Remove Doctor
    }
  }
}

