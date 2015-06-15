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
    public class EductionOfficeController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: EductionOffice/EductionOfficeIndex
        public ActionResult EductionOfficeIndex()
        {
            return View(db.EductionOffices.ToList());
        }

        /*
        // GET: EductionOffice/EductionOfficeDetails/5
        public ActionResult EductionOfficeDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EductionOffice eductionOffice = db.EductionOffices.Find(id);
            if (eductionOffice == null)
            {
                return HttpNotFound();
            }
            return View(eductionOffice);
        }
        */

        // GET: EductionOffice/EductionOfficeCreate
        public ActionResult EductionOfficeCreate()
        {
            return View();
        }

        // POST: EductionOffice/EductionOfficeCreate
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EductionOfficeCreate([Bind(Include = "EductionOfficeID,EductionOfficeName")] EductionOffice eductionOffice)
        {
            if (ModelState.IsValid)
            {
                db.EductionOffices.Add(eductionOffice);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a EductionOffice record");
                return RedirectToAction("EductionOfficeIndex");
            }

            DisplayErrorMessage();
            return View(eductionOffice);
        }

        // GET: EductionOffice/EductionOfficeEdit/5
        public ActionResult EductionOfficeEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EductionOffice eductionOffice = db.EductionOffices.Find(id);
            if (eductionOffice == null)
            {
                return HttpNotFound();
            }
            return View(eductionOffice);
        }

        // POST: EductionOfficeEductionOffice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EductionOfficeEdit([Bind(Include = "EductionOfficeID,EductionOfficeName")] EductionOffice eductionOffice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eductionOffice).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a EductionOffice record");
                return RedirectToAction("EductionOfficeIndex");
            }
            DisplayErrorMessage();
            return View(eductionOffice);
        }

        // GET: EductionOffice/EductionOfficeDelete/5
        public ActionResult EductionOfficeDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EductionOffice eductionOffice = db.EductionOffices.Find(id);
            if (eductionOffice == null)
            {
                return HttpNotFound();
            }
            return View(eductionOffice);
        }

        // POST: EductionOffice/EductionOfficeDelete/5
        [HttpPost, ActionName("EductionOfficeDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult EductionOfficeDeleteConfirmed(int id)
        {
            EductionOffice eductionOffice = db.EductionOffices.Find(id);
            db.EductionOffices.Remove(eductionOffice);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a EductionOffice record");
            return RedirectToAction("EductionOfficeIndex");
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
