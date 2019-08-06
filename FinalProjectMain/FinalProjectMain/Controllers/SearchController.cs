using FinalProjectMain.Models;
using FinalProjectMain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectMain.Controllers
{
    public class SearchController : Controller
    {
        ShoppingDbContext db = new ShoppingDbContext();

        
        // Main Search-Part

        public ActionResult clickSearchButton(string content)
        {
            var productsAll = db.products.Where(w =>w.DeletedDate==null &&w.isConfirm==true && w.Name.Contains(content) || content == null).AsNoTracking().ToList();
            var TakeProducts = productsAll.OrderByDescending(w=>w.Id).Take(9).ToList();
            saveProduct.saveProducts = productsAll;
            TempData["myList"] = TakeProducts;
            TempData["searchContent"] = content;
            return RedirectToAction("productsPage","Home");
        }

        // GET: Search
        public ActionResult searchInCategoryInTop()
        {
            var categories = db.categories.Where(w => w.DeletedDate == null).OrderBy(w=>w.CategoryName).ToList();
            return View(categories);
        }
        public ActionResult sortingABC(string value)
        {
            List<Product> currentItemsWithProducts = TempData["currentItemsWithProducts"] as List<Product>;
            List<CategoryAndProduct> currentItemsWithCategory = TempData["currentItemsWithCategory"] as List<CategoryAndProduct>;
            if (value == "ZtoA")
            {
                if (currentItemsWithCategory!=null)
                {
                    var productList = currentItemsWithCategory.OrderByDescending(w => w.Product.Name).ToList();
                    return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", productList);
                }
                else
                {
                    var sortingList = currentItemsWithProducts.OrderByDescending(w => w.Name).ToList();
                    return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingList);
                }

            }
            else 
            {
                if (currentItemsWithCategory != null)
                {
                    var productList = currentItemsWithCategory.OrderBy(w => w.Product.Name).ToList();
                    return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", productList);
                }
                else
                {
                    var sortingList = currentItemsWithProducts.OrderBy(w => w.Name).ToList();
                    return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingList);
                }
            }
            
        }

        
        public ActionResult sortingAccordion()
        {
            
            return View();
        }

        public ActionResult sortingAccordionClick(int? getColorValue,int? getPriceVal)
        {
            if (getColorValue!=null)
            {
                if (TempData["currentItemsWithProducts"] != null)
                {
                    if (getColorValue == 0)
                    {
                        var sortingListt = saveProduct.saveProducts.ToList();
                        return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingListt.OrderByDescending(w => w.Id).Take(9));
                    }
                    var sortingList = saveProduct.saveProducts.Where(w => w.color.Id == getColorValue).ToList();
                    return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingList.OrderByDescending(w => w.Id).Take(9));
                }

                else
                {
                    if (getColorValue == 0)
                    {
                        var sortingListt = saveProduct.saveProductsWithcategory.ToList();
                        return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", sortingListt.OrderByDescending(w => w.Id).Take(9));
                    }
                    var sortingList = saveProduct.saveProductsWithcategory.Where(w => w.Product.color.Id == getColorValue).ToList();
                    return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", sortingList.OrderByDescending(w => w.Id).Take(9));
                }
            }
            else
            {

                if (TempData["currentItemsWithProducts"] != null)
                {
                    if (getPriceVal==1)
                    {
                        var sortingList = saveProduct.saveProducts.Where(n => n.Price <= 100).ToList();
                        return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingList.OrderByDescending(w=>w.Id).Take(9));
                    }
                    else if (getPriceVal==2)
                    {
                        var sortingList = saveProduct.saveProducts.Where(n => n.Price >= 100 && n.Price<=200).ToList();
                        return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingList.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if (getPriceVal==3)
                    {
                        var sortingList = saveProduct.saveProducts.Where(n => n.Price >= 200 && n.Price <= 300).ToList();
                        return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingList.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if(getPriceVal == 4)
                    {
                        var sortingList = saveProduct.saveProducts.Where(n => n.Price >= 300 && n.Price <= 400).ToList();
                        return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingList.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if(getPriceVal == 5)
                    {
                        var sortingList = saveProduct.saveProducts.Where(n => n.Price >= 400 && n.Price <= 500).ToList();
                        return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingList.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if(getPriceVal == 6)
                    {
                        var sortingList = saveProduct.saveProducts.Where(n => n.Price >= 500 && n.Price <= 600).ToList();
                        return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingList.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if(getPriceVal == 7)
                    {
                        var sortingList = saveProduct.saveProducts.Where(n => n.Price >= 600 && n.Price <= 700).ToList();
                        return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingList.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if(getPriceVal == 8)
                    {
                        var sortingList = saveProduct.saveProducts.Where(n => n.Price >= 700 && n.Price <= 800).ToList();
                        return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingList.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if(getPriceVal == 9)
                    {
                        var sortingList = saveProduct.saveProducts.Where(n => n.Price >= 800 && n.Price <= 900).ToList();
                        return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingList.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if(getPriceVal==10)
                    {
                        var sortingList = saveProduct.saveProducts.Where(n => n.Price >= 900 && n.Price <= 1000).ToList();
                        return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingList.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if(getPriceVal==11)
                    {
                        var sortingList = saveProduct.saveProducts.Where(n => n.Price >= 1000 && n.Price <= 2000).ToList();
                        return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingList.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if(getPriceVal == 12)
                    {
                        var sortingList = saveProduct.saveProducts.Where(n => n.Price >= 2000).ToList();
                        return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingList.OrderByDescending(w => w.Id).Take(9));
                    }
                    else
                    {
                        var sortingList = saveProduct.saveProducts.ToList();
                        return PartialView("~/Views/Home/IntoproductsPage.cshtml", sortingList.OrderByDescending(w => w.Id).Take(9));
                    }

                }
                else
                {
                    if (getPriceVal == 1)
                    {
                        var sortingListt = saveProduct.saveProductsWithcategory.Where(n => n.Product.Price <= 100).ToList();
                        return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", sortingListt.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if (getPriceVal == 2)
                    {
                        var sortingListt = saveProduct.saveProductsWithcategory.Where(n => n.Product.Price >= 100 && n.Product.Price <= 200).ToList();
                        return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", sortingListt.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if (getPriceVal == 3)
                    {
                        var sortingListt = saveProduct.saveProductsWithcategory.Where(n => n.Product.Price >= 200 && n.Product.Price <= 300).ToList();
                        return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", sortingListt.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if (getPriceVal == 4)
                    {
                        var sortingListt = saveProduct.saveProductsWithcategory.Where(n => n.Product.Price >= 300 && n.Product.Price <= 400).ToList();
                        return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", sortingListt.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if (getPriceVal == 5)
                    {
                        var sortingListt = saveProduct.saveProductsWithcategory.Where(n => n.Product.Price >= 400 && n.Product.Price <= 500).ToList();
                        return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", sortingListt.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if (getPriceVal == 6)
                    {
                        var sortingListt = saveProduct.saveProductsWithcategory.Where(n => n.Product.Price >= 500 && n.Product.Price <= 600).ToList();
                        return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", sortingListt.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if (getPriceVal == 7)
                    {
                        var sortingListt = saveProduct.saveProductsWithcategory.Where(n => n.Product.Price >= 600 && n.Product.Price <= 700).ToList();
                        return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", sortingListt.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if (getPriceVal == 8)
                    {
                        var sortingListt = saveProduct.saveProductsWithcategory.Where(n => n.Product.Price >= 700 && n.Product.Price <= 800).ToList();
                        return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", sortingListt.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if (getPriceVal == 9)
                    {
                        var sortingListt = saveProduct.saveProductsWithcategory.Where(n => n.Product.Price >= 800 && n.Product.Price <= 900).ToList();
                        return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", sortingListt.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if (getPriceVal == 10)
                    {
                        var sortingListt = saveProduct.saveProductsWithcategory.Where(n => n.Product.Price >= 900 && n.Product.Price <= 1000).ToList();
                        return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", sortingListt.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if (getPriceVal == 11)
                    {
                        var sortingList = saveProduct.saveProductsWithcategory.Where(n => n.Product.Price >= 1000 && n.Product.Price <= 2000).ToList();
                        return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", sortingList.OrderByDescending(w => w.Id).Take(9));
                    }
                    else if (getPriceVal == 12)
                    {
                        var sortingList = saveProduct.saveProductsWithcategory.Where(n => n.Product.Price >= 2000).ToList();
                        return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", sortingList.OrderByDescending(w => w.Id).Take(9));
                    }
                    else
                    {
                        var sortingListt = saveProduct.saveProductsWithcategory.ToList();
                        return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", sortingListt.OrderByDescending(w => w.Id).Take(9));
                    }

                }
            }

        }


        public ActionResult searchWishlist(string content)
        {
            //todo wishlist
            var x = Session[SessionKey.User];
            var user = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email.ToString()==x.ToString()).FirstOrDefault();
            var search = db.wishListProductAndUsers.Where(w => w.DeletedDate == null && w.Products.Name.Contains(content)&&w.UserId==user.Id).ToList();
            return PartialView("~/Views/Home/wishListForEach.cshtml", search);
        }
    }
}