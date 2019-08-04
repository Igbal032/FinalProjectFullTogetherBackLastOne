using FinalProjectMain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectMain.Areas.Admin.Controllers
{
    public class CategoryColorCompanyController : Controller
    {
        // GET: Admin/CategoryColorCompany
        ShoppingDbContext db = new ShoppingDbContext();
        public ActionResult deletePage()
        {
            //var categories = db.categories.Where(w => w.DeletedDate == null).ToList();
            return View();
        }
        public ActionResult deleteCategory(int? id)
        {
            var checkSession = Session[SessionKey.Admin];
            var admin = db.Manager.Where(w => w.Email.ToString() == checkSession.ToString()).FirstOrDefault();
            var findcategory = db.categories.Where(w => w.DeletedDate == null && w.Id == id).FirstOrDefault();
            findcategory.DeletedDate = DateTime.Now;
            findcategory.DeletedId = admin.Id;
            db.Entry(findcategory).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return PartialView("~/Areas/Admin/Views/CategoryColorCompany/refleshPage.cshtml");
        }
        public ActionResult deleteSubCategory(int? id)
        {
            var checkSession = Session[SessionKey.Admin];
            var admin = db.Manager.Where(w => w.Email.ToString() == checkSession.ToString()).FirstOrDefault();
            var findSubcategory = db.SubCategories.Where(w => w.DeletedDate == null && w.Id == id).FirstOrDefault();
            findSubcategory.DeletedDate = DateTime.Now;
            findSubcategory.DeletedId = admin.Id;
            db.Entry(findSubcategory).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return PartialView("~/Areas/Admin/Views/CategoryColorCompany/refleshPage.cshtml");
        }
        public ActionResult deleteColor(int? id)
        {
            var checkSession = Session[SessionKey.Admin];
            var admin = db.Manager.Where(w => w.Email.ToString() == checkSession.ToString()).FirstOrDefault();
            var findColor = db.Color.Where(w => w.DeletedDate == null && w.Id == id).FirstOrDefault();
            findColor.DeletedDate = DateTime.Now;
            findColor.DeletedId = admin.Id;
            db.Entry(findColor).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return PartialView("~/Areas/Admin/Views/CategoryColorCompany/refleshPage.cshtml");
        }
        public ActionResult deleteCompany(int? id)
        {
            var checkSession = Session[SessionKey.Admin];
            var admin = db.Manager.Where(w => w.Email.ToString() == checkSession.ToString()).FirstOrDefault();
            var findCompany = db.producers.Where(w => w.DeletedDate == null && w.Id == id).FirstOrDefault();
            findCompany.DeletedDate = DateTime.Now;
            findCompany.DeletedId = admin.Id;
            db.Entry(findCompany).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return PartialView("~/Areas/Admin/Views/CategoryColorCompany/refleshPage.cshtml");
        }
    }
}