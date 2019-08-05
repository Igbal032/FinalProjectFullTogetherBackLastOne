using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProjectMain.Models;
using FinalProjectMain.Models.Entity;

namespace FinalProjectMain.Areas.Admin.Controllers
{
    public class EditProductsController : Controller
    {
        private ShoppingDbContext db = new ShoppingDbContext();
        public ActionResult Edit(int? id)
        {
            var findproduct = db.products.Find(id);
            return View(findproduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,About,Price,DiscountPrice,isWaitListForAdmin,isSold,LikeCount,starLevel,Comment,SubCategory,colorId,SizeORCount,isConfirm,ConfirmId,sharedDate,imgPath,ProducerId,SharedId,CreatedDate,CreatedId,ModifiedDate,ModifiedId,DeletedDate,DeletedId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.colorId = new SelectList(db.Color, "Id", "ColorName", product.colorId);
            ViewBag.ProducerId = new SelectList(db.producers, "Id", "ProducerName", product.ProducerId);
            ViewBag.SharedId = new SelectList(db.Manager, "Id", "Name", product.SharedId);
            return View(product);
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
