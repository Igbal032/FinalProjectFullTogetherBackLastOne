using FinalProjectMain.AppCode.Filters;
using FinalProjectMain.Models;
using FinalProjectMain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.HtmlControls;

namespace FinalProjectMain.Controllers
{
    [shoppingFilterAttributeForUser]
    [shoppingExceptionFilter]

    public class HomeController : Controller
    {
        ShoppingDbContext db = new ShoppingDbContext();
        // GET: Home

        [AllowAnonymous]
        public ActionResult Index()
        {

            return View();
        }
        [AllowAnonymous]
        public ActionResult activeUserIcon()
        {
            var x = Session[SessionKey.User];
            if (x != null)
            {
                var user = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email == x.ToString()).FirstOrDefault();
                return View(user);
            }
            return View();
        }

        //Main Crousel
        [AllowAnonymous]
        public ActionResult _mainCarousel()
        {
            var carouselPictures = db.mainCarousels.Where(w => w.DeletedDate == null).ToList();
            return View(carouselPictures);
        }
        // Main Carousel


        // Two new Products
        [AllowAnonymous]
        public ActionResult _twoNewProduct()
        {
            var product = db.CategoryAndProduct.Where(w => w.DeletedDate == null && w.Product.DeletedDate == null && w.Product.isConfirm == true && w.Category.CategoryName == "Yemək Dəsti").OrderByDescending(o=>o.ProductId).Take(2).ToList();
            return View(product);
        }
        // Two new Products
        
        // orderPage
        public ActionResult orderHistory()
        {
            var session = Session[SessionKey.User];
            var user = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email.ToString() == session.ToString()).FirstOrDefault();
            var orderHistories = db.OrderHistory.Where(w => w.DeletedDate == null && w.userId == user.Id &&w.user.isBlock==false&&w.user.DeletedDate==null).ToList();

            return View(orderHistories);
        }
        // orderPage


        // New Coming

        [AllowAnonymous]
        public ActionResult lastProducts()
        {
            IEnumerable<Product> lastProducts = db.products.OrderByDescending(o => o.Id).Take(8).Where(w => w.DeletedDate == null && w.isConfirm == true).ToList();
            return View(lastProducts);
        }

        [AllowAnonymous]
        public ActionResult lastProductsClick()
        {
            IEnumerable<Product> lastProducts = db.products.OrderByDescending(o => o.Id).Take(8).Where(w => w.DeletedDate == null && w.isConfirm == true).ToList();
            return PartialView("~/Views/Shared/_PartialViews/_lastProducts.cshtml",lastProducts);
        }
        [AllowAnonymous]
        public ActionResult _newProductTabContent()
        {
            var categories = db.categories.Take(2).Where(w => w.DeletedDate == null).ToList();
            return View(categories);
        }
        [AllowAnonymous]
        public ActionResult _newProductTab2Click(int? categoryId)
        {

            var findCategory = db.CategoryAndProduct.Where(w => w.DeletedDate == null && w.CategoryId == categoryId && w.Product.isConfirm == true).ToList();
            return PartialView("~/Views/Shared/_PartialViews/_newProductTabs.cshtml",findCategory);
        }

        // New Coming

        [AllowAnonymous]
        // Three new Products
        public ActionResult _threeNewProduct()
        {
            var products = db.products.OrderByDescending(w=>w.Id).Take(3).Where(w => w.DeletedDate == null).ToList();
            return View(products);
        }

        // Three new Products

        [AllowAnonymous]
        //Left-ProductTab
        public ActionResult _productsTabsLeft()
        {
            var categories = db.categories.Where(w => w.DeletedDate == null && (w.CategoryName == "Kompot Dəsti" || w.CategoryName == "Gümüş" || w.CategoryName == "Güldan")).ToList();
            
            return View(categories);
        }
        [AllowAnonymous]
        public ActionResult _clickproductsTabsLeft(int categoryId)
        {
            var products = db.CategoryAndProduct.Where(w => w.DeletedDate == null && w.CategoryId==categoryId&&w.Product.DeletedDate==null).ToList();
            
            return PartialView("~/Views/Shared/_PartialViews/_leftPartProductClick.cshtml", products);
        }
        //Left-ProductTab

        //right-part-of-Left-ProductTab
        [AllowAnonymous]
        public ActionResult _cardRightPart()
        {

            var item = db.products.Where(l => l.DeletedDate == null && l.isConfirm == true).FirstOrDefault();
            return View(item);
        }

        //right-part-of-Left-ProductTab
        [AllowAnonymous]
        // itemPage
        public ActionResult itemPage(int Id)
        {
            var findItem = db.products.Where(w => w.Id == Id).FirstOrDefault();
            var comments = db.comments.Where(w => w.DeletedDate == null && w.ProductId == Id).ToList();
            ViewBag.commentCount = comments.Count;
            return View(findItem);
        }
        // itemPage

        [AllowAnonymous]
        public ActionResult smallCarousel()
        {
            var products = new FinalProjectMain.Models.ShoppingDbContext().products.Where(w => w.DeletedDate == null && w.isConfirm == true).OrderByDescending(w => w.Id).Take(5).ToList();
            return View(products);
        }
        [AllowAnonymous]
        // releatedIten
        public ActionResult relatedItemsCarousel(int Id)
        {
            var thisItem = db.CategoryAndProduct.Where(w => w.ProductId == Id&&w.Product.DeletedDate==null&&w.Product.isConfirm==true).FirstOrDefault();
            var thisCategoryId = thisItem.CategoryId;
            var findRelatedGood = db.CategoryAndProduct.Where(w => w.CategoryId == thisCategoryId && w.Product.DeletedDate == null).ToList();
            return View(findRelatedGood);
        }
        // releatedIten
        [AllowAnonymous]
        // starlevel
        public ActionResult increseStarLevel(int id, int rate)
        {
            var find= db.rating.Where(w => w.DeletedDate == null && w.Product.Id == id).FirstOrDefault();

            if (find!=null)
            {
                find.starCount += 1;
                find.totalStarLevel += rate;
                find.currentStar = rate;
                int starLevel = calculatorRating.calStar(find.starCount, find.totalStarLevel);
                find.Product.starLevel = starLevel;
                db.Entry(find).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction($"itemPage/{id}");
            }
            else
            {
                Rating newRating = new Rating();
                newRating.ProductId = id;
                newRating.starCount += 1;
                newRating.currentStar = rate;
                newRating.totalStarLevel += rate;
                newRating.CreatedDate = DateTime.Now;
                db.rating.Add(newRating);
                db.SaveChanges();
            }
            return RedirectToAction($"itemPage/{id}");
        }
        // starlevel
        [AllowAnonymous]
        //smallImagesInOwlForItemPage
        public ActionResult itemPicturesOwlCarouseInItemPage(int Id)
        {
            var itemPictures = db.imageProducts.Where(w => w.DeletedDate == null && w.ProductId == Id&&w.Product.DeletedDate==null).ToList();
            return View(itemPictures);
        }
        //smallImagesInOwlForItemPage

        [AllowAnonymous]
        // Sale Product Part
        public ActionResult _discountProduct()
        {
            //TODO
            var categories = db.categories.Where( w=> w.DeletedDate==null && (w.CategoryName== "Yemək Dəsti" || w.CategoryName== "Çay Dəsti" || w.CategoryName== "Xrustal")).ToList();
            return View(categories);
        }

        [AllowAnonymous]
        public ActionResult discountProductTab()
        {
            var categoriesAndProduct = db.CategoryAndProduct.Where(w => w.DeletedDate == null && w.Category.CategoryName == "Yemək Dəsti"&& w.Product.isConfirm == true && w.Product.DeletedDate == null).ToList();
            return PartialView("~/Views/Shared/_PartialViews/_discountProductTab.cshtml",categoriesAndProduct);
        }
        [AllowAnonymous]
        public ActionResult clickDiscountProductTab(int? categoryId)
        {
            var categoriesAndProduct = db.CategoryAndProduct.Where(w => w.DeletedDate == null && w.CategoryId == categoryId).ToList();
            return PartialView("~/Views/Shared/_PartialViews/_discountProductTab.cshtml", categoriesAndProduct);
        }

        // Sale Product Part

        [AllowAnonymous]
        // _otherPart
        public ActionResult _otherPart()
        {
            var productandcategory = db.CategoryAndProduct.Where(w => w.DeletedDate == null && (w.Category.CategoryName == "Digər (Duz, Salfet qabı)" && w.Product.isConfirm==true&&w.Product.DeletedDate==null)).ToList();
            return View(productandcategory);
        }
        // _otherPart
        [AllowAnonymous]
        // ContactUs
        public ActionResult contactUs()
        {

            return View();
        }
        // ContactUs
        [AllowAnonymous]
        //SendMessageContactUs
        [HttpPost]
        public ActionResult ContactUs(string subject, string email, string message, HttpPostedFileBase file)
        {
            if (String.IsNullOrWhiteSpace(subject) || String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(message))
            {
                TempData["fillInput"] = "Zəhmət Olmasa Xanaları Doldurun !!!";
                return View();
            }
            string SaveLocation = "";
            string fileName = "";
            if (file != null)
            {

                fileName = System.IO.Path.GetFileName(file.FileName);
                SaveLocation = Server.MapPath("~/ContactFiles/") + "\\" + fileName;
                try
                {
                    file.SaveAs(SaveLocation);
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }

            }

            var newMessage = new ContactUS()
            {
                Subject = subject,
                Email = email,
                Message = message,
                filePath = fileName,
                CreatedDate = DateTime.Now,
                answeredDate = DateTime.Now,
                //CreatedId = Todo
            };
            db.ContactUs.Add(newMessage);

            MailExtention.SendMail(newMessage.Subject, newMessage.Message, "iqbal.hoff@list.ru");
            db.SaveChanges();
            return View("Index");
        }
        // SendMessageContactUs

        // myAccount
        public ActionResult myAccount()
        {

            return View();
        }
        // myAccount

        // myAccount
        [AllowAnonymous]
        public ActionResult addAdressToAccount()
        {

            return View();
        }
        // myAccount

        // countryListInAddAdress
        [AllowAnonymous]
        public ActionResult countryListInAddAdress()
        {

            return View(db.Countries.Where(w=>w.DeletedDate==null).ToList());
        }
        // countryListInAddAdress

        // myAccountSubmit
        [AllowAnonymous]
        public ActionResult addAdressToAccountSubmit(addAdressToAccount addNewAdress)
        {
            if (addNewAdress.CountryId == 0)
            {
                TempData["selectCountry"] = "Ölkə Seçin";
                return View("addAdressToAccount");
            }
            if (ModelState.IsValid || addNewAdress.Countries==null)
            {

                    var session = Session[SessionKey.Admin];
                    var checkUser = db.user.Where(w => w.Email.ToString() == session.ToString()).FirstOrDefault();
                    addNewAdress.CreatedDate = DateTime.Now;
                    addNewAdress.UserId = checkUser.Id;
                    addNewAdress.CreatedDate = DateTime.Now;
                    db.addAdressToAccounts.Add(addNewAdress);
                    db.SaveChanges();
                    return View("myAccount");

            }
            else
            {
                return View("addAdressToAccount");
            }
           
        }
        // myAccountSubmit

        // infoPartMyAccount
        public ActionResult infoPartMyAccount()
        {
            var checkSession = Session[SessionKey.User];
            var user = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email == checkSession.ToString()).FirstOrDefault();
            return View(user);
        }

        // infoPartMyAccount

        // changeMyAccount
        public ActionResult changeMyAccount(int GenderId, string UserName,string UserSurname,
            string Email,string Password, string repeatedPassword, DateTime BirtDay, bool? Notifi)
        {
            var checkSession = Session[SessionKey.User];
            var user = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email == checkSession.ToString()).FirstOrDefault();
            if (Password != repeatedPassword)
            {

                TempData["wrongPassword"] = "Şifrəni Düzgün Təstiqləyin";
                return View("infoPartMyAccount", user);
            }
            else
            {
                user.UserName = UserName;
                user.UserSurname = UserSurname;
                user.GenderId = GenderId;
                user.Email = Email;
                user.Password = Password;
                user.BirthDate = BirtDay;
                if (Notifi==null)
                {
                    user.Notification = false;
                }
                else
                {
                    user.Notification = Notifi;
                }
                user.Notification = Notifi;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                TempData["changed"] = "Melumatlar Yeniləndi";
            }
            return View("infoPartMyAccount", user);
        }
        // changeMyAccount


        [AllowAnonymous]
        // productsPage
        public ActionResult productsPage()
        {

            return View();
        }
        // productsPage
        [AllowAnonymous]
        // IntoproductsPage
        public ActionResult IntoproductsPage()

        {
            if (TempData["myList"]!=null)
            {
                return PartialView("~/Views/Home/IntoproductsPage.cshtml", TempData["myList"]);
            }
            else if (TempData["selectedCategory"] != null)
            {
                
                return PartialView("~/Views/Shared/_PartialViews/_intoProductPageForCategory.cshtml", TempData["selectedCategory"]);

            }
            var mixedProduct = db.products.Where(w => w.DeletedDate == null && w.isConfirm == true && w.isSold == false).OrderByDescending(o => o.sharedDate).Take(9).ToList();
            saveProduct.saveProducts = db.products.Where(w => w.DeletedDate == null && w.isConfirm == true && w.isSold == false).OrderByDescending(o => o.sharedDate).ToList();
            int count = mixedProduct.Count;
            TempData["count1"] = count;
            TempData["model"] = mixedProduct;

            return PartialView("~/Views/Home/IntoproductsPage.cshtml", mixedProduct);
        }

        // IntoproductsPage
        [AllowAnonymous]
        //pagination-Part
        public ActionResult pagePagination(int? pageId)
        {
            IEnumerable<Product> nextPage;
            IEnumerable<CategoryAndProduct> nextPagee;
            if (TempData["currentItemsWithProducts"] != null)
            {
                if (pageId!=null)
                {

                    int leftPage = int.Parse(((pageId - 1) * 9).ToString());

                    ViewBag.count = saveProduct.saveProducts.Count / 9;
                    var count = ViewBag.count;

                    if (saveProduct.saveProducts.Count % 9>0)
                    {
                        count = count + 1;
                        ViewBag.count = count;
                    }
                    nextPage = saveProduct.saveProducts.Where(w => w.DeletedDate == null && w.isConfirm == true).OrderByDescending(w => w.Id).Skip(leftPage).Take(9).ToList();
                    return PartialView("~/Views/Home/IntoproductsPage.cshtml", nextPage);
                    
                }
                else
                {
                    ViewBag.count = saveProduct.saveProducts.Count / 9;
                    var count = ViewBag.count;

                    if (saveProduct.saveProducts.Count % 9 > 0)
                    {
                        count = count + 1;
                        ViewBag.count = count;
                    }
                    nextPage = saveProduct.saveProducts.Where(w => w.DeletedDate == null && w.isConfirm == true).OrderByDescending(w => w.Id).Take(9).ToList();
                    TempData["currentItemsWithCategory"] = null;
                    TempData["currentItemsWithProducts"] = nextPage;
                    return PartialView("~/Views/Home/pagePagination.cshtml");
                }
            }
            else if (TempData["currentItemsWithCategory"]!= null)
            {
                if (pageId != null)
                {
                    int leftPage = int.Parse(((pageId - 1) * 9).ToString());
                    ViewBag.count = saveProduct.saveProductsWithcategory.Count / 9;
                    var count = ViewBag.count;

                    if (saveProduct.saveProductsWithcategory.Count % 9> 0)
                    {
                        count = count + 1;
                        ViewBag.count = count;
                    }

                    if (ViewBag.count == 0)
                    {
                        ViewBag.count = 1;
                    }
                    nextPagee = saveProduct.saveProductsWithcategory.Where(w => w.Product.DeletedDate == null && w.Product.isConfirm == true).OrderByDescending(w => w.Id).Skip(leftPage).Take(9).ToList();
                    return PartialView("~/Views/Shared/_PartialViews/intoProductPageForCategory.cshtml", nextPagee);
                }
                else
                {
                    ViewBag.count = saveProduct.saveProductsWithcategory.Count / 9;
                    var count = ViewBag.count;

                    if (saveProduct.saveProductsWithcategory.Count % 9 > 0)
                    {
                        count = count + 1;
                        ViewBag.count = count;
                    }
                    if (ViewBag.count==0)
                    {
                        ViewBag.count = 1;
                    }
                    nextPagee = saveProduct.saveProductsWithcategory.Where(w => w.Product.DeletedDate == null && w.Product.isConfirm == true).OrderByDescending(w => w.Id).Take(9).ToList();
                    TempData["currentItemsWithCategory"] = nextPagee;
                    TempData["currentItemsWithProducts"] = null;
                    return PartialView("~/Views/Home/pagePagination.cshtml");
                }
            }
            else
            {

                return PartialView("~/Views/Home/IntoproductsPage.cshtml", db.products.Where(w=>w.DeletedDate==null&&w.isConfirm==true).OrderByDescending(w => w.Id).Take(9).ToList());
            }

        }
        //pagination-Part
        // productsPage

        [AllowAnonymous]
        // shopping-Adress
        public ActionResult shoppingAdress()
        {
            var shoppingAdress = db.shoppingAdresses.Where(w => w.DeletedDate == null).ToList();
            return View(shoppingAdress);
        }
        // shopping-Adress

        //Register Part
        [AllowAnonymous]
        public ActionResult registerPart()
        {

            return View();
        }
        // Register Part

        //Register Part Submit
        [AllowAnonymous]
        public ActionResult registerPartSubmit(User newUser)
        {
            if (ModelState.IsValid)
            {
                var checkUser = db.user.Any(w => w.DeletedDate == null && w.Email == newUser.Email);
                if (checkUser)
                {
                    TempData["userExists"] = "Bu Email ilə artıq İstifadəçi var";
                    return View("registerPart");
                }
                else
                {

                    newUser.CreatedDate = DateTime.Now;
                    var age = DateTime.Now.Year - newUser.BirthDate.Year;
                    newUser.UserAge = age.ToString();
                    db.user.Add(newUser);
                    if (newUser.Notification == true)
                    {
                        var newSubscribe = new Subscribe
                        {
                            Email = newUser.Email,
                            CreatedDate = DateTime.Now,
                            confirmDate = DateTime.Now,
                            isConfirm = true,

                        };
                        db.Subscribes.Add(newSubscribe);
                        db.SaveChanges();
                    }
                    TempData["Registered"] = "Email və Şifrəni Yazıb Daxil Olun";
                    return RedirectToAction("logIn");
                }
            }
            else
            {
                return View("registerPart");
            }

        }
        // Register Part Submit

        //Log In
        [AllowAnonymous]
        public ActionResult logIn()
        {

            return View();
        }

        [AllowAnonymous]
        //[Route("{home}/{index}")]
        public ActionResult logInSubmit([Bind(Include = ("Email,Password"))]User User)
        {
            if (!(string.IsNullOrWhiteSpace(User.Email) || string.IsNullOrWhiteSpace(User.Password)))
            {
                var findUser = db.user.Where(w => w.DeletedDate == null && w.Email == User.Email).FirstOrDefault();

                if (findUser != null)
                {

                    if (findUser.Password == User.Password)
                    {
                        if (findUser.isBlock == true)
                        {
                            TempData["isBlock"] = "Sizin Hesab Bloklanıb!!";
                            return RedirectToAction("logIn");

                        }
                        Session[SessionKey.User] = User.Email;
                        var active = Session[SessionKey.User];

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["wrongPassword"] = "Şifrə Yanlışdır!!";
                        return View("logIn");
                    }
                }
                var x = Session[SessionKey.User];

                return RedirectToAction("Index", "Home");
            }
            else
            {

                return View("logIn");
            }
        }
        // Log In

        [AllowAnonymous]
        public ActionResult forgetPasswordPage()
        {

            return View();
        }
        [AllowAnonymous]
        public ActionResult forgetPasswordSubmit(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                return View("forgetPasswordPage");
            }
            else
            {
                var userEmail = db.user.Where(w => w.DeletedDate == null && w.Email == user.Email).FirstOrDefault();
                if (userEmail == null)
                {
                    TempData["emaildoesnexist"] = "Email tapılmadı!!";
                    return View("logIn");
                }
                if (userEmail.isBlock == true)
                {
                    TempData["blockUser"] = "Siza admin tərəfdən bloklanmısınız!!";
                    return View("logIn");
                }
                else
                {
                    MailExtention.SendMail("Şifrə Yeniləmə", $"Sizin Shifrəniz {userEmail.Password}!!", userEmail.Email);
                    TempData["sendEmailForResetPassword"] = "Sizin şifrə email - ə göndərildi";
                    return View("logIn");
                }
            }

        }


        // LogOutUser

        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        //LogOutUser

        [AllowAnonymous]
        //Like Click
        public ActionResult clickLike(int productId)
        {
            if (Session[SessionKey.User] != null)
            {
                int currentUserId = 0;
                var users = db.user.Where(w => w.DeletedDate == null && w.isBlock == false).ToList();
                foreach (var item in users)
                {
                    if (item.Email.ToString() == Session[SessionKey.User].ToString())
                    {
                        currentUserId = item.Id;
                    }
                }
                var product = db.products.Where(w => w.DeletedDate == null && w.isConfirm == true && w.Id == productId).FirstOrDefault();
                var checkUser = db.Likes.Where(w => w.DeletedDate == null && w.ProductId == productId && w.UserId == currentUserId).FirstOrDefault();
                if (checkUser != null)
                {
                    if (product.LikeCount > 0)
                    {
                        product.LikeCount = product.LikeCount - 1;
                        checkUser.DeletedDate = DateTime.Now;
                        db.Entry(checkUser).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        product.LikeCount = 0;
                    }
                }
                else
                {
                    var newLike = new Like();
                    newLike.ProductId = productId;
                    newLike.UserId = currentUserId;
                    newLike.CreatedDate = DateTime.Now;
                    db.Likes.Add(newLike);
                    product.LikeCount = product.LikeCount + 1;
                }

                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                var count = product.LikeCount;
                return Json(count, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("notSession", JsonRequestBehavior.AllowGet);
            }

        }
        // Like Click
        [AllowAnonymous]
        //SubscribeButton
        public ActionResult subscribeButton(string email)
        {
            bool checkEmail = db.Subscribes.Any(w => w.DeletedDate == null && w.Email == email);
            if (!checkEmail)
            {
                var newSubscribe = new Subscribe();
                newSubscribe.Email = email;
                newSubscribe.CreatedDate = DateTime.Now;
                db.Subscribes.Add(newSubscribe);
                db.SaveChanges();
                MailExtention.SendMail("Subscribe Confirmation", "https://localhost:44354/home/confirmSubscribe/" + newSubscribe.Id, email);
                return View("Index");
            }
            else
            {
                TempData["existsEmailInSubscribe"] = "Bu email mövcuddur!";
                return View("Index");
            }
        }

        [AllowAnonymous]
        public ActionResult confirmSubscribe(int Id)
        {
            var subscribeUser = db.Subscribes.Where(w => w.DeletedDate == null && w.Id == Id).FirstOrDefault();
            subscribeUser.isConfirm = true;
            subscribeUser.confirmDate = DateTime.Now;
            db.Entry(subscribeUser).State = EntityState.Modified;
            db.SaveChanges();
            return View("Index");
        }

    }
}