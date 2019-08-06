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
        public ActionResult EditSubmit(int? id, string name,decimal price, string about, List<HttpPostedFileBase>imgFiles)
        {
            var findProduct = db.products.Find(id);
            findProduct.Name = name;
            findProduct.Price = price;
            findProduct.About = about;
            if (imgFiles[0]!=null)
            {
                string SaveLocation = "";

                foreach (var file in imgFiles)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    SaveLocation = Server.MapPath("~/Template/img/newImg/") + "\\" + fileName;
                    try
                    {
                        var newImgPath = new imageProduct();
                        newImgPath.ProductId = findProduct.Id;
                        newImgPath.imgPath = fileName;
                        newImgPath.CreatedDate = DateTime.Now;
                        db.imageProducts.Add(newImgPath);
                        file.SaveAs(SaveLocation);
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error: " + ex.Message);
                    }
                }
            }
            db.Entry(findProduct).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("newAddProduct","Admin");
        }

        public ActionResult deleteImage(int? imageId,int? productId)
        {
            var findImage = db.imageProducts.Find(imageId);
            findImage.DeletedDate = DateTime.Now;
            db.Entry(findImage).State = EntityState.Modified;
            db.SaveChanges();
            IEnumerable<imageProduct> imageList = db.imageProducts.Where(w => w.DeletedDate == null&&w.ProductId== productId).ToList();
            return PartialView("~/Areas/Admin/Views/Shared/PartialView/_productImage.cshtml", imageList);
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
