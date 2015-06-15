using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webscaffolder.Areas.School.Models;

namespace webscaffolder.Areas.School.Controllers
{
    public class SchoolController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: School/Index
        public ActionResult Index()
        {
            var school = db.Schools.Include(s => s.EductionOffice).Include(s => s.Gender).Include(s => s.Level).Include(s => s.SchoolType);
            return View(school.ToList());
        }

        /*
        // GET: School/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }
        */

        // GET: School/Create
        public ActionResult Create()
        {
            ViewBag.EductionOfficeId = new SelectList(db.EductionOffices, "EductionOfficeID", "EductionOfficeName");
            ViewBag.GenderId = new SelectList(db.SchoolGenders, "GenderID", "GenderName");
            ViewBag.levelId = new SelectList(db.SchoolLevels, "LevelID", "LevelName");
            ViewBag.SchoolTypeId = new SelectList(db.SchoolTypes, "SchoolTypeID", "SchoolTypeName");
            return View();
        }

        // POST: School/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EductionOffice,Gender,Level,SchoolType,Number,Name,ManagerName,ManagerAasistName,GenderId,EductionOfficeId,SchoolTypeId,levelId,Email")] School.Models.School school)
        {
            if (ModelState.IsValid)
            {
                db.Schools.Add(school);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a School record");
                return RedirectToAction("Index");
            }

            ViewBag.EductionOfficeId = new SelectList(db.EductionOffices, "EductionOfficeID", "EductionOfficeName", school.EductionOfficeId);
            ViewBag.GenderId = new SelectList(db.SchoolGenders, "GenderID", "GenderName", school.GenderId);
            ViewBag.levelId = new SelectList(db.SchoolLevels, "LevelID", "LevelName", school.levelId);
            ViewBag.SchoolTypeId = new SelectList(db.SchoolTypes, "SchoolTypeID", "SchoolTypeName", school.SchoolTypeId);
            DisplayErrorMessage();
            return View(school);
        }

        // GET: School/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School.Models.School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            ViewBag.EductionOfficeId = new SelectList(db.EductionOffices, "EductionOfficeID", "EductionOfficeName", school.EductionOfficeId);
            ViewBag.GenderId = new SelectList(db.SchoolGenders, "GenderID", "GenderName", school.GenderId);
            ViewBag.levelId = new SelectList(db.SchoolLevels, "LevelID", "LevelName", school.levelId);
            ViewBag.SchoolTypeId = new SelectList(db.SchoolTypes, "SchoolTypeID", "SchoolTypeName", school.SchoolTypeId);
            return View(school);
        }

        // POST: School/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EductionOffice,Gender,Level,SchoolType,Number,Name,ManagerName,ManagerAasistName,GenderId,EductionOfficeId,SchoolTypeId,levelId,Email")] School.Models.School school)
        {
            if (ModelState.IsValid)
            {
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a School record");
                return RedirectToAction("Index");
            }
            ViewBag.EductionOfficeId = new SelectList(db.EductionOffices, "EductionOfficeID", "EductionOfficeName", school.EductionOfficeId);
            ViewBag.GenderId = new SelectList(db.SchoolGenders, "GenderID", "GenderName", school.GenderId);
            ViewBag.levelId = new SelectList(db.SchoolLevels, "LevelID", "LevelName", school.levelId);
            ViewBag.SchoolTypeId = new SelectList(db.SchoolTypes, "SchoolTypeID", "SchoolTypeName", school.SchoolTypeId);
            DisplayErrorMessage();
            return View(school);
        }

        // GET: School/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School.Models.School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: School/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            School.Models.School school = db.Schools.Find(id);
            db.Schools.Remove(school);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a School record");
            return RedirectToAction("Index");
        }

        private void DisplaySuccessMessage(string msgText)
        {
            TempData["SuccessMessage"] = msgText;
        }

        private void DisplayErrorMessage()
        {
            TempData["ErrorMessage"] = "Save changes was unsuccessful.";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
