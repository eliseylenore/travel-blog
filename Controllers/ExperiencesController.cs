using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelBlog.Controllers
{
    
    public class ExperiencesController : Controller
    {
        private TravelBlogContext db = new TravelBlogContext();
        public IActionResult Index()
        {
            return View(db.Experiences.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.PlaceId = new SelectList(db.Places, "PlaceId", "Name");
            ViewBag.PersonId = new SelectList(db.People, "PersonId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Experience experience)
        {
            db.Experiences.Add(experience);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            
            var thisExperience = db.Experiences.FirstOrDefault(experiences => experiences.ExperienceId == id);
            ViewBag.Person = db.People.FirstOrDefault(people => people.PersonId == thisExperience.PersonId);
            return View(thisExperience);
        }
    }
}
