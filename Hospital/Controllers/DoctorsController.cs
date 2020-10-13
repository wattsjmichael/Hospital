using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital.Models;

namespace Hospital.Controllers
{
  public class DoctorsController : Controllers
  {
    private readonly HospitalContext _db;

    public DoctorsController(HospitalContext db)
    {
      _db = db;
    }
  public ActionResult Index()
  {
    return View(_db.Doctors.ToList());
  }
  }

