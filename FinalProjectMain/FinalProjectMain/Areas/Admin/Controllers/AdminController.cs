using FinalProjectMain.Models;
using FinalProjectMain.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectMain.Areas.Admin.Controllers
{
    [shoppingFilter]
    [shoppingExceptionFilterAttribute]
    public class AdminController : Controller
    {
        ShoppingDbContext db = new ShoppingDbContext();
        // GET: Admin/Admin


        public ActionResult Index()
        {
            var checkSession = Session[SessionKey.Admin];
            var admin = db.Manager.Where(w => w.DeletedDate == null && w.Email.ToString() == checkSession.ToString()).FirstOrDefault();
            if (admin.ManagerStatus.StatusName!="SuperAdmin")
            {
                TempData["notAllow"] = "notAllow";
            }
            return View();
        }
        public ActionResult CreateNewItem()
        {

            return View();
        }
        [HttpPost]
        public ActionResult SubmitCreateNewItem(string name, string about, int producerVal, int colorVal,
            List<int> categoryVal, int? multiCategoryVal, decimal size, decimal price, decimal discountPrice, List<HttpPostedFileBase> imgFiles)
        {
            if (categoryVal == null && multiCategoryVal == null)
            {
                TempData["emptyCategory"] = "Məhsulun Kategoriyasını daxil edin!!";
                return View("CreateNewItem");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["emptyNameInput"] = "Məhsulun adını daxil edin";
                return View("CreateNewItem");
            }
            if (imgFiles[0] != null && (categoryVal != null || multiCategoryVal != 0))
            {
                bool findUser = false;
                Manager findSuperAdmin = new Manager();
                Manager findAdmin = new Manager();
                var newProduct = new Product();
                newProduct.Name = name;
                newProduct.About = about;
                newProduct.ProducerId = producerVal;
                newProduct.colorId = colorVal;
                newProduct.SizeORCount = size;
                newProduct.Price = price;
                newProduct.DiscountPrice = discountPrice;
                if (multiCategoryVal != null)
                {
                    var findMultiCategory = db.SubCategories.Where(w => w.DeletedDate == null && w.Id == multiCategoryVal).FirstOrDefault();
                    newProduct.SubCategory = findMultiCategory.SubCategoryName;
                }
                newProduct.isWaitListForAdmin = true;
                var checkAdmin = db.Manager.Where(w =>w.DeletedDate==null&&w.ManagerStatus.DeletedDate==null&& w.ManagerStatus.StatusName == "SuperAdmin").ToList();
                foreach (var item in checkAdmin)
                {
                    if (Session[SessionKey.Admin].ToString() == item.Email)
                    {
                        findSuperAdmin = item;
                        findUser = true;
                    }
                }
                if (findUser==true)
                {
                    newProduct.CreatedId = findSuperAdmin.Id;
                    newProduct.SharedId = findSuperAdmin.Id;
                    newProduct.isConfirm = true;
                    newProduct.sharedDate = DateTime.Now;
                    var subscribeUsers = db.Subscribes.Where(w => w.DeletedDate == null && w.isConfirm == true).ToList();
                    foreach (var user in subscribeUsers)
                    {
                        MailExtention.SendMail("Yeni Məhsul", newProduct.Name + " " + newProduct.Price + "AZN", user.Email);
                    }
                }
                else
                {
                    var checkSession = Session[SessionKey.Admin];
                    var findActiveAdmin = db.Manager.Where(w => w.DeletedDate == null && w.Email.ToString() == checkSession.ToString()).FirstOrDefault();
                    newProduct.isConfirm = false;
                    newProduct.SharedId = findActiveAdmin.Id;

                }
                newProduct.CreatedDate = DateTime.Now;
                newProduct.imgPath = imgFiles.FirstOrDefault().FileName;
                db.products.Add(newProduct);
                foreach (var categoryItem in categoryVal)
                {
                    var newCategoryAndProduct = new CategoryAndProduct();
                    newCategoryAndProduct.CategoryId = categoryItem;
                    newCategoryAndProduct.ProductId = newProduct.Id;
                    db.CategoryAndProduct.Add(newCategoryAndProduct);
                }

                string SaveLocation = "";

                foreach (var file in imgFiles)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    SaveLocation = Server.MapPath("~/Template/img/newImg/") + "\\" + fileName;
                    try
                    {
                        var newImgPath = new imageProduct();
                        newImgPath.ProductId = newProduct.Id;
                        newImgPath.imgPath = fileName;
                        newImgPath.CreatedDate = DateTime.Now;
                        db.imageProducts.Add(newImgPath);
                        db.SaveChanges();
                        file.SaveAs(SaveLocation);
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error: " + ex.Message);
                    }

                }
                db.SaveChanges();
                var newProductOrWaitList = db.products.OrderByDescending(o => o.Id).Where(p => p.DeletedDate == null && (p.isWaitListForAdmin == true && p.isConfirm == false));
                return PartialView("~/Areas/Admin/Views/Admin/newAddProduct.cshtml", newProductOrWaitList);
            }
            else
            {
                TempData["imgIsEmpty"] = "Şəkil Seçin !!";
                return View("CreateNewItem");
            }
        }
        public ActionResult newAddProduct()
        {
            bool findUser = false;
            Manager findSuperAdmin = new Manager();
            var checkAdmin = db.Manager.Where(w => w.DeletedDate == null && w.ManagerStatus.DeletedDate == null && w.ManagerStatus.StatusName == "SuperAdmin").ToList();
            foreach (var item in checkAdmin)
            {
                if (Session[SessionKey.Admin].ToString() == item.Email)
                {
                    findSuperAdmin = item;
                    findUser = true;
                }
            }
            if (findUser==true)
            {
                TempData["admin"] = "admin";
            }
            var newProductOrWaitList = db.products.OrderByDescending(o => o.Id).Where(p => p.DeletedDate == null && (p.isWaitListForAdmin == true && p.isConfirm == false));
            return View("newAddProduct", newProductOrWaitList.ToList());
        }
        public ActionResult newAddProductForEach()
        {
            var newProductOrWaitList = db.products.OrderByDescending(o => o.Id).Where(p => p.DeletedDate == null && (p.isWaitListForAdmin == true && p.isConfirm == false));
            return View(newProductOrWaitList.ToList());
        }
        public ActionResult LookAtAllİmages(int id)
        {
            var newAllImagesInWaitList = db.imageProducts.OrderByDescending(o => o.Id).Where(p => p.DeletedDate == null && p.ProductId == id);
            return View(newAllImagesInWaitList.ToList());
        }
        public ActionResult deleteItemFromWaitList(int id)
        {

            var item = db.products.Where(w => w.DeletedDate == null && w.Id == id).FirstOrDefault();
            var activeSession = Session[SessionKey.Admin];
            var findAdmin = db.Manager.Where(w => w.DeletedDate == null && w.Email == activeSession.ToString()).FirstOrDefault();
            item.DeletedDate = DateTime.UtcNow;
            item.DeletedId = findAdmin.Id;
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return PartialView("~/Areas/Admin/Views/Admin/newAddProductForEach.cshtml", db.products.OrderByDescending(o => o.Id).Where(w => w.DeletedDate == null && (w.isWaitListForAdmin == true && w.isConfirm == false)).ToList());
        }
        public ActionResult deleteItemFromSharedItem(int id)
        {
            var item = db.products.Where(w => w.DeletedDate == null && w.Id == id).FirstOrDefault();
            var activeSession = Session[SessionKey.Admin];
            var findAdmin = db.Manager.Where(w => w.DeletedDate == null && w.Email == activeSession.ToString()).FirstOrDefault();
            item.DeletedDate = DateTime.UtcNow;
            item.DeletedId = findAdmin.Id;
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            var products = db.products.OrderByDescending(o => o.Id).Where(w => w.DeletedDate == null && w.isConfirm == true).ToList();
            return PartialView("~/Areas/Admin/Views/Shared/PartialView/_deleteProductsInSharedItems.cshtml", products);
        }
        public ActionResult confirmItemFromWaitList(int id)
        {
            var activeSession = Session[SessionKey.Admin];
            var admin = db.Manager.Where(w => w.DeletedDate == null && w.Email == activeSession.ToString()).FirstOrDefault();
            var item = db.products.Where(w => w.DeletedDate == null && w.Id == id).FirstOrDefault();
            item.isConfirm = true;
            item.sharedDate = DateTime.Now;
            item.ConfirmId = admin.Id;
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            var subscribeUsers = db.Subscribes.Where(w => w.DeletedDate == null && w.isConfirm == true).ToList();
            foreach (var user in subscribeUsers)
            {
                MailExtention.SendMail("Yeni Məhsul", item.Name + " " + item.Price, user.Email);
            }

            return PartialView("~/Areas/Admin/Views/Admin/newAddProductForEach.cshtml", db.products.OrderByDescending(o => o.Id).Where(w => w.DeletedDate == null && (w.isWaitListForAdmin == true && w.isConfirm == false)).ToList());
        }

        public ActionResult CategorySelectInCreateItem()
        {
            var categories = db.categories.Where(c => c.DeletedDate == null).OrderBy(w=>w.CategoryName).ToList();
            return PartialView("~/Areas/Admin/Views/Admin/CategorySelectInCreateItem.cshtml", categories);
        }
        public ActionResult MultiCategorySelectInCreateItem()
        {
            return View();
        }
        public ActionResult FillMultiCategorySelectInCreateItem(int? categoryId)
        {
            List<SubCategory> subcategories = db.SubCategories.Where(c => c.DeletedDate == null && c.CategoryId == categoryId).OrderBy(w=>w.SubCategoryName).ToList();
            if (subcategories.Count != 0)
            {

                return PartialView("~/Areas/Admin/Views/Admin/MultiCategorySelectInCreateItem.cshtml", subcategories);
            }
            return Json("null", JsonRequestBehavior.AllowGet);

        }
        public ActionResult addNewCategory(string categoryname, int? withSubCategory)
        {
            bool checkCategory = db.categories.Any(w => w.DeletedDate == null && w.CategoryName == categoryname);

            if (checkCategory)
            {

                return Json("theSameName", JsonRequestBehavior.AllowGet);
            }
            if (String.IsNullOrWhiteSpace(categoryname))
            {

                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var activeSession = Session[SessionKey.Admin];
                var admin = db.Manager.Where(w => w.DeletedDate == null && w.Email == activeSession.ToString()).FirstOrDefault();
                var newCategory = new Category();
                {
                    newCategory.CategoryName = categoryname;
                    if (withSubCategory == 1)
                    {
                        newCategory.withSubCtegory = true;
                    }
                    else if (withSubCategory == 2)
                    {
                        newCategory.withSubCtegory = false;
                    }
                    newCategory.CreatedDate = DateTime.Now;
                    newCategory.CreatedId = admin.Id;
                };

                db.categories.Add(newCategory);
                db.SaveChanges();
                return PartialView("~/Areas/Admin/Views/Admin/CategorySelectInCreateItem.cshtml", db.categories.Where(w => w.DeletedDate == null).OrderBy(w=>w.CategoryName).ToList());
            }

        }
        public ActionResult addNewSubCategory(int? categoryId, string subcategoryname)
        {
            if (categoryId == null)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            bool checkSubCategory = db.SubCategories.Any(w => w.DeletedDate == null && w.CategoryId == categoryId && w.SubCategoryName == subcategoryname);

            if (checkSubCategory)
            {

                return Json("theSameName", JsonRequestBehavior.AllowGet);
            }
            if (String.IsNullOrWhiteSpace(subcategoryname))
            {

                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var activeSession = Session[SessionKey.Admin];
                var admin = db.Manager.Where(w => w.DeletedDate == null && w.Email == activeSession.ToString()).FirstOrDefault();
                var newSubCategory = new SubCategory()
                {
                    SubCategoryName = subcategoryname,
                    CategoryId = categoryId,
                    CreatedDate = DateTime.Now,
                    CreatedId = admin.Id,
                };

                db.SubCategories.Add(newSubCategory);
                db.SaveChanges();
                return PartialView("~/Areas/Admin/Views/Admin/MultiCategorySelectInCreateItem.cshtml", db.SubCategories.Where(w => w.DeletedDate == null && w.CategoryId == categoryId).OrderBy(w=>w.SubCategoryName).ToList());
            }

        }
        public ActionResult addNewColor(string colorname)
        {
            bool checkColor = db.Color.Any(w => w.DeletedDate == null && w.ColorName == colorname);

            if (checkColor)
            {

                return Json("theSameName", JsonRequestBehavior.AllowGet);
            }
            if (String.IsNullOrWhiteSpace(colorname))
            {

                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var activeSession = Session[SessionKey.Admin];
                var admin = db.Manager.Where(w => w.DeletedDate == null && w.Email == activeSession.ToString()).FirstOrDefault();
                var newColor = new Color()
                {
                    ColorName = colorname,
                    CreatedDate = DateTime.Now,
                    CreatedId = admin.Id,
                };

                db.Color.Add(newColor);
                db.SaveChanges();
                return PartialView("~/Areas/Admin/Views/Admin/ColorInCreateItem.cshtml", db.Color.Where(w => w.DeletedDate == null).OrderBy(w=>w.ColorName).ToList());
            }

        }
        public ActionResult addNewProducer(string producername)
        {
            bool checkProducer = db.producers.Any(w => w.DeletedDate == null && w.ProducerName == producername);

            if (checkProducer)
            {
                return Json("theSameName", JsonRequestBehavior.AllowGet);
            }
            if (String.IsNullOrWhiteSpace(producername))
            {

                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var activeSession = Session[SessionKey.Admin];
                var admin = db.Manager.Where(w => w.DeletedDate == null && w.Email == activeSession.ToString()).FirstOrDefault();
                var newProducer = new ProducerCompany()
                {
                    ProducerName = producername,
                    CreatedDate = DateTime.Now,
                    CreatedId = admin.Id,
                };

                db.producers.Add(newProducer);
                db.SaveChanges();
                return PartialView("~/Areas/Admin/Views/Admin/ProducerInCreateItem.cshtml", db.producers.Where(w => w.DeletedDate == null).OrderBy(w=>w.ProducerName).ToList());
            }

        }
        public ActionResult ProducerInCreateItem()
        {
            var producers = db.producers.Where(p => p.DeletedDate == null).OrderBy(w=>w.ProducerName).ToList();
            return PartialView("~/Areas/Admin/Views/Admin/ProducerInCreateItem.cshtml", producers);
        }
        public ActionResult ColorInCreateItem()
        {
            var colors = db.Color.Where(c => c.DeletedDate == null).OrderBy(w=>w.ColorName).ToList();
            return PartialView("~/Areas/Admin/Views/Admin/ColorInCreateItem.cshtml", colors);
        }
        public ActionResult userslist()
        {

            var userslist = db.user.Where(w => w.DeletedDate == null).OrderBy(w=>w.Id).ToList();
            return View(userslist);
        }
        public ActionResult ignoreUser(int id)
        {

            var user = db.user.Where(w => w.DeletedDate == null && w.Id == id).FirstOrDefault();
            user.isBlock = true;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            var userslist = db.user.Where(w => w.DeletedDate == null).OrderBy(w => w.Id).ToList();
            return PartialView("~/Areas/Admin/Views/Admin/usersListForeach.cshtml", userslist);
        }
        public ActionResult notignoreUser(int id)
        {

            var user = db.user.Where(w => w.DeletedDate == null && w.Id == id).FirstOrDefault();
            user.isBlock = false;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            var userslist = db.user.Where(w => w.DeletedDate == null).OrderBy(w => w.Id).ToList();
            return PartialView("~/Areas/Admin/Views/Admin/usersListForeach.cshtml", userslist);
        }
        public ActionResult usersListForeach()
        {

            var userslist = db.user.Where(w => w.DeletedDate == null).OrderBy(w=>w.Id).ToList();
            return View(userslist);
        }
        public ActionResult employeeList()
        {

            var employeelist = db.Manager.Where(w => w.DeletedDate == null).OrderBy(w=>w.Id).ToList();
            return View(employeelist);
        }
        public ActionResult employeeListForeach()
        {

            var employeelist = db.Manager.Where(w => w.DeletedDate == null).OrderBy(w => w.Id).ToList();
            return View(employeelist);
        }
        public ActionResult DeleteEmployeeInList(int id)
        {
            var deletedEmploye = db.Manager.Where(w => w.DeletedDate == null && w.Id == id).FirstOrDefault();
            deletedEmploye.DeletedDate = DateTime.UtcNow;
            db.Entry(deletedEmploye).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return PartialView("~/Areas/Admin/Views/Admin/employeeListForeach.cshtml", db.Manager.Where(w => w.DeletedDate == null).OrderBy(w => w.Id).ToList());
        }
        public ActionResult editEmployeeInList(int id)
        {
            var editEmployee = db.Manager.Where(w => w.DeletedDate == null && w.Id == id).FirstOrDefault();

            return Json(editEmployee, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditEmployeeInListSubmit(Manager editmanager)
        {
            var activeSession = Session[SessionKey.Admin];
            var admin = db.Manager.Where(w => w.DeletedDate == null && w.Email == activeSession.ToString()).FirstOrDefault();

            editmanager.ModifiedDate = DateTime.UtcNow;
            editmanager.ModifiedId = admin.Id;
            db.Entry(editmanager).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            var employeList = db.Manager.Where(w => w.DeletedDate == null).OrderBy(w => w.Id).ToList();
            return PartialView("~/Areas/Admin/Views/Admin/employeeListForeach.cshtml", employeList);
        }
        public ActionResult addAdminPage()
        {

            return View();
        }
        public ActionResult SubmitAddAdminPage(Manager newManager)
        {
            if (ModelState.IsValid)
            {
                var checkNewManager = db.Manager.Any(m => m.DeletedDate == null && (m.Email == newManager.Email || m.Phone == newManager.Phone));
                if (checkNewManager)
                {
                    TempData["existsManager"] = "Email mövcuddur";
                    return View("addAdminPage");
                }
                newManager.CreatedDate = DateTime.Now;
                db.Manager.Add(newManager);
                db.SaveChanges();
                TempData["SaveNewManager"] = "Yeni Admin istifadəçisi yaradıldı";
                return View("Index");
            }
            else
            {
                return View("addAdminPage");
            }

        }
        public ActionResult statusInCreateAdmin()
        {

            var managerStatuses = db.ManagerStatus.Where(m => m.DeletedDate == null).OrderBy(w => w.StatusName).ToList();
            return PartialView("~/Areas/Admin/Views/Admin/statusInCreateAdmin.cshtml", managerStatuses);
        }
        public ActionResult addNewStatus(string statusname)
        {
            bool checkStatus = db.ManagerStatus.Any(w => w.StatusName == statusname);

            if (checkStatus)
            {

                return Json("theSameName", JsonRequestBehavior.AllowGet);
            }
            if (String.IsNullOrWhiteSpace(statusname))
            {

                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var activeSession = Session[SessionKey.Admin];
                var admin = db.Manager.Where(w => w.DeletedDate == null && w.Email == activeSession.ToString()).FirstOrDefault();
                var newStatus = new ManagerStatus()
                {
                    StatusName = statusname,
                    CreatedDate = DateTime.Now,
                    CreatedId = admin.Id,
                };

                db.ManagerStatus.Add(newStatus);
                db.SaveChanges();
                return PartialView("~/Areas/Admin/Views/Admin/statusInCreateAdmin.cshtml", db.ManagerStatus.Where(w => w.DeletedDate == null).OrderBy(w => w.StatusName).ToList());
            }
        }
        public ActionResult sharedItemss()
        {

            var sharedItems = db.products.Where(m => m.DeletedDate == null && m.isConfirm == true).OrderByDescending(o => o.Id).ToList();
            return View(sharedItems);
        }
        [AllowAnonymous]
        public ActionResult messageIconAndnotificationIcon()
        {

            return View();
        }
        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult messageIcon()
        {
            var x = Session[SessionKey.Admin];
            if (x != null)
            {
                var unreadMessages = db.ContactUs.OrderByDescending(o => o.Id).Where(m => m.DeletedDate == null && m.isRead == false).ToList();
                return View(unreadMessages);
            }
            else
            {
                return View();
            }
        }
        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult notificationIcon()

        {
            var x = Session[SessionKey.Admin];
            if (x != null)
            {
                var newItemsNotification = db.products.OrderByDescending(o => o.Id).Where(m => m.DeletedDate == null && m.isWaitListForAdmin == true && m.isConfirm == false).ToList();
                return View(newItemsNotification);
            }
            else
            {
                return View();
            }

        }
        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult accountPart()
        {
            var x = Session[SessionKey.Admin];

            if (x != null)
            {
                var admin = db.Manager.Where(m => m.DeletedDate == null && m.Email == x.ToString()).FirstOrDefault();
                return View(admin);
            }
            else
            {
                return View();
            }

        }
        public ActionResult changePasswordAdmin()
        {

            return View();
        }
        public ActionResult changePasswordAdminSubmit(string currentPassword, string newPassword, string confirmNewPassword)
        {
            var activeSession = Session[SessionKey.Admin];
            var findAdmin = db.Manager.Where(w => w.DeletedDate == null && w.Email == activeSession.ToString()).FirstOrDefault();
            if (currentPassword != findAdmin.Password)
            {
                TempData["wrongOldPassword"] = "Şifrə yanlışdır!!";
                return View("changePasswordAdmin");
            }
            if (currentPassword == findAdmin.Password && (newPassword==""||confirmNewPassword==""))
            {
                TempData["writePassword"] = "Yeni şifrəni daxil edin!!";
                return View("changePasswordAdmin");
            }
            else
            {
                if (confirmNewPassword != newPassword)
                {
                    TempData["wrongConfirmNewPassword"] = "Təstiq Şifrəsi yanlışdır!!";
                    return View("changePasswordAdmin");
                }
                else
                {
                    findAdmin.Password = confirmNewPassword;
                    db.Entry(findAdmin).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["changePassword"] = "Şifrəni dəyişdirildi!!";
                }
            }
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult LogOutAdmin()
        {
            Session.Abandon();
            return RedirectToAction("LoginPartForAdmin", "Admin");
        }
        [AllowAnonymous]
        public ActionResult LoginPartForAdmin()
        {
            var x = Session[SessionKey.Admin];
            return View();
        }
        [AllowAnonymous]
        public ActionResult LoginPartForAdminSubmit([Bind(Include = "Email, Password")]Manager manager)
        {
            if (!(string.IsNullOrWhiteSpace(manager.Email) || string.IsNullOrWhiteSpace(manager.Password)))
            {
                var findAdmin = db.Manager.Where(w => w.DeletedDate == null && w.Email == manager.Email).FirstOrDefault();
                if (findAdmin != null)
                {
                    if (manager.Password == findAdmin.Password)
                    {
                        var newAdminSession = new SessionKey();
                        Session[SessionKey.Admin] = manager.Email;
                        var x = Session[SessionKey.Admin];
                    }
                    else
                    {
                        TempData["wrongPassword"] = "Yanlış Şifrə";
                        return View("LoginPartForAdmin");
                    }
                }
                else
                {
                    TempData["emaildoesnexist"] = "Email tapılmadı";
                    return View("LoginPartForAdmin");
                }
                return RedirectToAction("Index", "Admin");
            }
            else
            {

                return RedirectToAction("LoginPartForAdmin", "Admin");
            }

        }
        public ActionResult mainCarousel()
        {
            var carouselimg = db.mainCarousels.OrderByDescending(w=>w.Id).Where(w => w.DeletedDate == null).ToList();
            return View(carouselimg);
        }
        [AllowAnonymous]
        public ActionResult deleteMainCarouselPicture(int? pictureId)
        {
            var checkSession = Session[SessionKey.Admin];
            var findAdmin = db.Manager.Where(w => w.DeletedDate == null && w.Email.ToString() == checkSession.ToString()).FirstOrDefault();
            var findPicture = db.mainCarousels.Where(w => w.DeletedDate == null&&w.Id==pictureId).FirstOrDefault();
            findPicture.DeletedDate = DateTime.Now;
            findPicture.DeletedId = findAdmin.Id;
            db.Entry(findPicture).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            var carouselimh = db.mainCarousels.OrderByDescending(w => w.Id).Where(w => w.DeletedDate == null).ToList();

            return PartialView("~/Views/Shared/_PartialViews/_mainCarouselPictures.cshtml", carouselimh);
        }
        [AllowAnonymous]
        public ActionResult changeMainCarousel(string title,  HttpPostedFileBase imgPath)
        {
            if (imgPath != null)
            {

                string SaveLocation = "";


                string fileName = Path.GetFileName(imgPath.FileName);
                SaveLocation = Server.MapPath("~/Template/img/mainCarousel/") + "\\" + fileName;

                var carouselPicture = new mainCarousel();
                carouselPicture.imgPath = fileName;
                if (title !=null)
                {
                    carouselPicture.title = title;
                }
                else
                {
                    carouselPicture.title = null;
                }
                carouselPicture.CreatedDate = DateTime.Now;
                db.mainCarousels.Add(carouselPicture);
                db.SaveChanges();
                imgPath.SaveAs(SaveLocation);

                db.SaveChanges();

                var carouselimh = db.mainCarousels.OrderByDescending(w => w.Id).Where(w => w.DeletedDate == null).ToList();
                return View("mainCarousel", carouselimh);
            }
            else
            {
                TempData["imgIsEmpty"] = "Şəkil Seçin !!";
                var carouselimh = db.mainCarousels.OrderByDescending(w => w.Id).Where(w => w.DeletedDate == null).ToList();
                return View("mainCarousel", carouselimh);
            }
        }

        [AllowAnonymous]
        public ActionResult forgetPasswordPage()
        {

            return View();
        }
        [AllowAnonymous]
        public ActionResult submitForgetPasswordButton(string Email)
        {
            if (Email == "")
            {
                TempData["empty"] = "Zəhmət olmasa xananı doldurun!!";
                return View("forgetPasswordPage");
            }
            else
            {
                var findEmail = db.Manager.Where(w => w.DeletedDate == null && w.Email == Email).FirstOrDefault();
                if (findEmail != null)
                {
                    TempData["sentEmail"] = "Şifrə email - ə göndərildi";
                    MailExtention.SendMail("Şifrə", "Sizin şifrə " + findEmail.Password, findEmail.Email);
                    return View("LoginPartForAdmin");
                }
                else
                {
                    TempData["emaildoesntexists"] = "Email tapılmadı";
                    return View("forgetPasswordPage");
                }
            }
        }
    }
}