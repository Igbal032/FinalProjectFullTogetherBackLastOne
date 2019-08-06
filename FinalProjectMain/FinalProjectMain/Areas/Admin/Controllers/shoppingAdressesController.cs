using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProjectMain.Models;
using FinalProjectMain.Models.Entity;

namespace FinalProjectMain.Areas.Admin.Controllers
{
    [shoppingFilter]
    public class shoppingAdressesController : Controller
    {
        private ShoppingDbContext db = new ShoppingDbContext();

        // GET: Admin/shoppingAdresses
        public ActionResult Index()
        {
            return View(db.shoppingAdresses.ToList());
        }

        // GET: Admin/shoppingAdresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shoppingAdresses shoppingAdresses = db.shoppingAdresses.Find(id);
            if (shoppingAdresses == null)
            {
                return HttpNotFound();
            }
            return View(shoppingAdresses);
        }

        // GET: Admin/shoppingAdresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/shoppingAdresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,About,Adress,imgPath,CreatedDate,CreatedId,ModifiedDate,ModifiedId,DeletedDate,DeletedId")] shoppingAdresses shoppingAdresses, HttpPostedFileBase imgPath)
        {
            if (ModelState.IsValid)
            {
                string SaveLocation = "";

                if (imgPath!=null)
                {
                    string fileName = Path.GetFileName(imgPath.FileName);
                    SaveLocation = Server.MapPath("~/Template/img/Adresses/") + "\\" + fileName;
                    try
                    {

                        shoppingAdresses.imgPath = fileName;
                        imgPath.SaveAs(SaveLocation);
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error: " + ex.Message);
                    }
                }
                shoppingAdresses.CreatedDate = DateTime.Now;
                db.shoppingAdresses.Add(shoppingAdresses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shoppingAdresses);
        }

        // GET: Admin/shoppingAdresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shoppingAdresses shoppingAdresses = db.shoppingAdresses.Find(id);
            if (shoppingAdresses == null)
            {
                return HttpNotFound();
            }
            return View(shoppingAdresses);
        }

        // POST: Admin/shoppingAdresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,About,Adress,imgPath,CreatedDate,CreatedId,ModifiedDate,ModifiedId,DeletedDate,DeletedId")] shoppingAdresses shoppingAdresses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingAdresses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shoppingAdresses);
        }

        // GET: Admin/shoppingAdresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            shoppingAdresses shoppingAdresses = db.shoppingAdresses.Find(id);
            if (shoppingAdresses == null)
            {
                return HttpNotFound();
            }
            return View(shoppingAdresses);
        }

        // POST: Admin/shoppingAdresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            shoppingAdresses shoppingAdresses = db.shoppingAdresses.Find(id);
            shoppingAdresses.DeletedDate = DateTime.Now;
            db.Entry(shoppingAdresses).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
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
