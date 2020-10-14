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
      .FirstOrDefault(doctor => doctor.DoctorId == id);
      return View(ThisDoctor);
    }
  }
}

