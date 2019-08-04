using FinalProjectMain.AppCode.Filters;
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
    [shoppingFilterAttributeForUser]
    public class additionalActionsController : Controller
    {
        ShoppingDbContext db = new ShoppingDbContext();


        // myWishList
        public ActionResult myWishList()
        {
            var x = Session[SessionKey.User];
            var checkUser = db.user.Where(w => w.isBlock == false && w.DeletedDate == null && w.Email.ToString() == x.ToString()).FirstOrDefault();
            var wishList = db.wishListProductAndUsers.Where(w => w.DeletedDate == null && w.UserId == checkUser.Id).ToList();
            return View("myWishList", wishList);
        }
        // myWishList
        // addToMyWishList
        [AllowAnonymous]
        public ActionResult addToMyWishListt(int id)
        {
            var checkUser = Session[SessionKey.User];
            if (checkUser != null)
            {
                var findUser = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email.ToString() == checkUser.ToString()).FirstOrDefault();
                var checkProduct = db.wishListProductAndUsers.Any(w => w.DeletedDate == null && w.ProductsId == id);
                if (checkProduct)
                {
                    return Json("existsProduct", JsonRequestBehavior.AllowGet);
                }
                var newWishList = new wishListProductAndUser();
                newWishList.UserId = findUser.Id;
                newWishList.ProductsId = id;
                newWishList.CreatedDate = DateTime.Now;
                db.wishListProductAndUsers.Add(newWishList);
                db.SaveChanges();
                return Json(newWishList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("nosession", JsonRequestBehavior.AllowGet);
            }

        }
        [AllowAnonymous]
        public ActionResult deleteWishList(int id)
        {
            var x = Session[SessionKey.User];
            var findUser = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email.ToString() == x.ToString()).FirstOrDefault();
            var find = db.wishListProductAndUsers.Where(w => w.DeletedDate == null && w.Id == id).FirstOrDefault();
            find.DeletedDate = DateTime.Now;
            db.Entry(find).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            var wishlist = db.wishListProductAndUsers.Where(w => w.DeletedDate == null && w.UserId == findUser.Id).ToList();
            return PartialView("~/Views/Shared/_PartialViews/_wishListForeach.cshtml", wishlist);
        }
        // addToMyWishList
        public ActionResult viewCard()
        {
            var checkUser = Session[SessionKey.User];
            var findUser = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email.ToString() == checkUser.ToString()).FirstOrDefault();
            var shopList = db.shopList.Where(w => w.DeletedDate == null && w.UserId == findUser.Id && w.isSold == false).ToList();
            return View(shopList);
        }
        public ActionResult openAdressPart()
        {
            var session = Session[SessionKey.User];
            var findUser = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email.ToString() == session.ToString()).FirstOrDefault();
            var adress = db.addAdressToAccounts.Where(w => w.DeletedDate == null && w.UserId == findUser.Id).ToList();
            return View(adress);
        }
        public ActionResult chosenAdress(int? adressId)
        {

            var adress = db.addAdressToAccounts.Where(w => w.DeletedDate == null && w.Id == adressId).FirstOrDefault();
            return View(adress);
        }
        public ActionResult submitOrderButton()
        {
            var session = Session[SessionKey.User];
            var findUser = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email.ToString() == session.ToString()).FirstOrDefault();
            var orderedProducts = db.shopList.Where(w => w.DeletedDate == null && w.UserId == findUser.Id).ToList();
            foreach (var orderProduct in orderedProducts)
            {

                var newOrder = new OrderHistory();
                newOrder.productId = orderProduct.Product.Id;
                newOrder.userId = findUser.Id;
                newOrder.CreatedDate = DateTime.Now;
                db.OrderHistory.Add(newOrder);
                orderProduct.isSold = true;
                db.Entry(orderProduct).State = System.Data.Entity.EntityState.Modified;
                var currentProduct = db.products.Where(w => w.DeletedDate == null && w.isConfirm == true && w.Id == orderProduct.Product.Id).FirstOrDefault();
                currentProduct.isSold = true;
                db.Entry(currentProduct).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult addToShopList(int productId,int? newProductId)
        {
            //todo
            var checkUser = Session[SessionKey.User];
            if (checkUser != null)
            {
                var findUser = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email.ToString() == checkUser.ToString()).FirstOrDefault();
                var findProduct = db.shopList.Where(w => w.DeletedDate == null && w.ProductId == productId && w.UserId == findUser.Id && w.isSold == false).FirstOrDefault();
                var product = db.products.Any(w => w.DeletedDate == null && w.isConfirm == true && w.isSold == true && w.Id == productId);
                if (product)
                {
                    return Json("alreadySold", JsonRequestBehavior.AllowGet);
                }
                if (findProduct != null)
                {
                    return Json("alreadyInShopList", JsonRequestBehavior.AllowGet);
                }

                else
                {
                    var newShop = new shopList();
                    newShop.ProductId = productId;
                    newShop.UserId = findUser.Id;
                    newShop.productCount = 1;
                    db.shopList.Add(newShop);
                    db.SaveChanges();

                    var products = db.shopList.Where(w => w.DeletedDate == null && w.UserId == findUser.Id && w.isSold == false).AsNoTracking().ToList();
                    ViewBag.itemCount = products.Count;
                    if (newProductId!=null)
                    {
                        return PartialView("~/Views/Shared/_PartialViews/_shopListForNavIcon.cshtml", products);
                    }
                    return PartialView("~/Views/Shared/_PartialViews/_intoShopIcon.cshtml", products);
                }
            }
            else
            {
                return Json("nosession", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult deleteFromShopList(int? id,int? idInShopIcon,int? idInShopNavId)
        {

            if (id!=null)
            {
                var checkUser = Session[SessionKey.User];
                var findUser = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email.ToString() == checkUser.ToString()).FirstOrDefault();
                var findProduct = db.shopList.Where(w => w.DeletedDate == null && w.Id == id).FirstOrDefault();
                findProduct.DeletedDate = DateTime.Now;
                db.Entry(findProduct).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                var shopList = db.shopList.Where(w => w.DeletedDate == null && w.UserId == findUser.Id && w.isSold == false).AsNoTracking().ToList();
                return PartialView("~/Views/Shared/_PartialViews/_shopList.cshtml", shopList);
            }
            if (idInShopIcon!=null)
            {
                var checkUser = Session[SessionKey.User];
                var findUser = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email.ToString() == checkUser.ToString()).FirstOrDefault();
                var findProduct = db.shopList.Where(w => w.DeletedDate == null && w.Id == idInShopIcon).FirstOrDefault();
                findProduct.DeletedDate = DateTime.Now;
                db.Entry(findProduct).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                var shopList = db.shopList.Where(w => w.DeletedDate == null && w.UserId == findUser.Id && w.isSold == false).AsNoTracking().ToList();
                return PartialView("~/Views/Shared/_PartialViews/_shopListForIcon.cshtml", shopList);
            }
            if (idInShopNavId != null)
            {
                var checkUser = Session[SessionKey.User];
                var findUser = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email.ToString() == checkUser.ToString()).FirstOrDefault();
                var findProduct = db.shopList.Where(w => w.DeletedDate == null && w.Id == idInShopNavId).FirstOrDefault();
                findProduct.DeletedDate = DateTime.Now;
                db.Entry(findProduct).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                var shopList = db.shopList.Where(w => w.DeletedDate == null && w.UserId == findUser.Id && w.isSold == false).AsNoTracking().ToList();
                return PartialView("~/Views/Shared/_PartialViews/_shopListForNavIcon.cshtml", shopList);
            }
            else
            {
                return RedirectToAction("Index","Home");

            }

        }
        [AllowAnonymous]
        public ActionResult shopIcon()
        {
            if (Session[SessionKey.User] != null)
            {
                var checkUser = Session[SessionKey.User];
                var findUser = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email.ToString() == checkUser.ToString()).FirstOrDefault();
                var shopList = db.shopList.Where(w => w.DeletedDate == null && w.UserId == findUser.Id && w.isSold == false).ToList();
                ViewBag.itemCount = shopList.Count;
                return View(shopList);

            }
            else
            {
                return View();
            }
        }
        [AllowAnonymous]
        public ActionResult shopIconInNavbar()
        {
            if (Session[SessionKey.User] != null)
            {
                var checkUser = Session[SessionKey.User];
                var findUser = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email.ToString() == checkUser.ToString()).FirstOrDefault();
                var shopList = db.shopList.Where(w => w.DeletedDate == null && w.UserId == findUser.Id && w.isSold == false).ToList();
                ViewBag.itemCount = shopList.Count;
                return View(shopList);

            }
            else
            {
                return View();
            }
        }
    }
}