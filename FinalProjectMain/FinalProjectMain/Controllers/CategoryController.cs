using FinalProjectMain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectMain.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        ShoppingDbContext db = new ShoppingDbContext();
        // GET: Admin/Category

        public ActionResult allCategoriesInNavBar()
        {
            var Category = db.categories.Where(w => w.DeletedDate == null).ToList();
            return View(Category);
        }
        public ActionResult allCategoriesItemPage()
        {
            var Category = db.categories.Where(w => w.DeletedDate == null).ToList();
            return View(Category);
        }

        public ActionResult intoSideBar()
        {
            var categories = db.categories.Where(w => w.DeletedDate == null).ToList();
            return View(categories);
        }

        public ActionResult intoAllCategories()
        {
            var categories = db.categories.Where(w => w.DeletedDate == null).ToList();
            return PartialView("~/Views/Category/intoAllCategories.cshtml", categories);
        }
        public ActionResult clickCategoryToProductPage(int? categoryId, int? subCategoryId)
        {
            if (subCategoryId != null)
            {
                var findCategory = db.SubCategories.Where(w => w.DeletedDate == null && w.Id == subCategoryId).FirstOrDefault();
                var products = db.CategoryAndProduct.Where(w => w.DeletedDate == null&&w.Product.DeletedDate==null && w.Product.isConfirm == true && w.Product.SubCategory == findCategory.SubCategoryName&&w.CategoryId==findCategory.CategoryId).ToList();
                saveProduct.saveProductsWithcategory = products;
                var takeProducts = products.OrderByDescending(w => w.Id).Take(9);
                if (products.Count == 0)
                {
                    return Json("emptyProducts", JsonRequestBehavior.AllowGet);
                }
                return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", takeProducts);
            }
            else
            {
                var findCategory = db.categories.Where(w => w.DeletedDate == null && w.Id == categoryId).FirstOrDefault();
                var products = db.CategoryAndProduct.Where(w => w.Product.DeletedDate == null && w.Product.DeletedDate==null && w.Product.isConfirm == true && w.CategoryId == categoryId).ToList();
                saveProduct.saveProductsWithcategory = products;
                var takeProducts = products.OrderByDescending(w => w.Id).Take(9);
                if (products.Count == 0)
                {
                    return Json("emptyProducts", JsonRequestBehavior.AllowGet);
                }
                return PartialView("~/Views/Shared/_PartialViews/intoProductPageForCategory.cshtml", takeProducts);
            }
        }

        public ActionResult findProducts(string categoryName, int? subCategoryId)
        {
            //todo on nav category
            if (subCategoryId != null)
            {

                var findSubcategory = db.SubCategories.Where(w => w.DeletedDate == null && w.Id == subCategoryId).FirstOrDefault();
                var products = db.CategoryAndProduct.Where(w => w.DeletedDate == null && w.Product.DeletedDate == null && w.Product.isConfirm == true && w.Product.SubCategory == findSubcategory.SubCategoryName && w.CategoryId == findSubcategory.CategoryId).ToList();
                var findproduct = db.products.Where(w => w.DeletedDate == null && w.isConfirm == true && w.SubCategory == findSubcategory.SubCategoryName).ToList();
                TempData["findsProducts"] = findproduct;
                return RedirectToAction("productsPage", "Home");
            }
            else
            {
                var findCategory = db.categories.Where(w => w.DeletedDate == null && w.CategoryName == "Yemək Dəsti" || w.CategoryName == "Çay Dəsti").FirstOrDefault();
                var findproduct = db.CategoryAndProduct.Where(w => w.DeletedDate == null && w.Product.isConfirm == true && w.Product.DeletedDate == null && w.CategoryId == findCategory.Id).ToList();
                return View();
            }
        }
        public ActionResult findProduct(int? souvenir, int? idSub, int? serviz, int? qashiq, int? bokal, int? categoryId)
        {

            if (souvenir != null)
            {
                var products = db.CategoryAndProduct.Where(w => w.DeletedDate == null && w.Category.DeletedDate == null && w.Product.DeletedDate == null && w.Category.CategoryName == "Hədiyəlik Suvenir").ToList();
                saveProduct.saveProductsWithcategory = products;
                TempData["selectedCategory"] = products.OrderByDescending(w=>w.Id).Take(9).ToList();
            }
            else if (idSub != null)
            {
                var subcategory = db.SubCategories.Where(w => w.DeletedDate == null && w.Id == idSub).FirstOrDefault();
                var products = db.CategoryAndProduct.Where(w => w.DeletedDate == null && w.Product.DeletedDate == null && w.Product.isConfirm == true && w.Product.SubCategory == subcategory.SubCategoryName && w.CategoryId == subcategory.CategoryId).ToList();
                //saveProduct.saveProducts = products;
                saveProduct.saveProductsWithcategory = products;
                TempData["selectedCategory"] = products.OrderByDescending(w => w.Id).Take(9).ToList();
                //TempData["myList"] = products.OrderByDescending(w => w.Id).Take(9).ToList();
            }
            else if (serviz != null)
            {
                var products = db.CategoryAndProduct.Where(w => w.DeletedDate == null && w.Category.DeletedDate == null && w.Product.DeletedDate == null && (w.Category.CategoryName == "Yemək Dəsti" || w.Category.CategoryName == "Çay Dəsti")).ToList();
                saveProduct.saveProductsWithcategory = products;
                TempData["selectedCategory"] = products.OrderByDescending(w => w.Id).Take(9).ToList();
            }
            else if (qashiq != null)
            {
                var products = db.CategoryAndProduct.Where(w => w.DeletedDate == null && w.Category.DeletedDate == null && w.Product.DeletedDate == null && (w.Category.CategoryName == "Qaşıq")).ToList();
                saveProduct.saveProductsWithcategory = products;
                TempData["selectedCategory"] = products.OrderByDescending(w => w.Id).Take(9).ToList();
            }
            else if (bokal != null)
            {
                var products = db.CategoryAndProduct.Where(w => w.DeletedDate == null && w.Category.DeletedDate == null && w.Product.DeletedDate == null && (w.Category.CategoryName == "Bokal")).ToList();
                saveProduct.saveProductsWithcategory = products;
                TempData["selectedCategory"] = products.OrderByDescending(w => w.Id).Take(9).ToList();
            }
            else if (categoryId != null)
            {
                var products = db.CategoryAndProduct.Where(w => w.DeletedDate == null && w.Category.DeletedDate == null && w.Product.DeletedDate == null && w.CategoryId == categoryId).ToList();
                saveProduct.saveProductsWithcategory = products;
                TempData["selectedCategory"] = products.OrderByDescending(w => w.Id).Take(9).ToList();
            }
            return RedirectToAction("productsPage", "Home");
        }
    }
}