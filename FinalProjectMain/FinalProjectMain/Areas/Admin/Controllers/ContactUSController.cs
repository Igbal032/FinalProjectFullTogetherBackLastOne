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
    [shoppingFilter]
    public class ContactUSController : Controller
    {
        private ShoppingDbContext db = new ShoppingDbContext();

        // GET: Admin/ContactUS
        public ActionResult Index()
        {
            return View(db.ContactUs.OrderByDescending(w=>w.Id).Where(w=>w.DeletedDate==null).ToList());
        }

        public ActionResult listofmessage()
        {
            var allMessages = db.ContactUs.OrderByDescending(w => w.Id).Where(w => w.DeletedDate == null).ToList();
            return View(allMessages);
        }
        public ActionResult lookAtMessage(int? id)
        {
            var currentMessage = db.ContactUs.Where(w => w.DeletedDate == null && w.Id == id).FirstOrDefault();
            if (currentMessage.isRead==false)
            {
                currentMessage.isRead = true;
                db.Entry(currentMessage).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            var allMessages = db.ContactUs.OrderByDescending(w => w.Id).Where(w => w.DeletedDate == null).ToList();
            return Json(currentMessage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult sendAnswerToUser(ContactUS contact)
        {
            var currentMessage = db.ContactUs.Where(w => w.DeletedDate == null && w.Id == contact.Id).FirstOrDefault();
            currentMessage.AnsweredMessage = contact.AnsweredMessage;
            currentMessage.isAnswered = true;
            currentMessage.answeredDate = DateTime.Now;
            db.Entry(currentMessage).State = EntityState.Modified;
            db.SaveChanges();
            MailExtention.SendMail(contact.Subject, contact.AnsweredMessage, contact.Email);
            var allMessages = db.ContactUs.OrderByDescending(w => w.Id).Where(w => w.DeletedDate == null).ToList();
            return PartialView("~/Areas/Admin/Views/ContactUS/listofmessage.cshtml", allMessages);
        }

        public ActionResult closeTheModalInContact()
        {
            var allMessages = db.ContactUs.OrderByDescending(w => w.Id).Where(w => w.DeletedDate == null).ToList();
            return PartialView("~/Areas/Admin/Views/ContactUS/listofmessage.cshtml", allMessages);
        }

        public ActionResult deleteMessage(int id)
        {
            var currentMessage = db.ContactUs.Where(w => w.DeletedDate == null && w.Id == id).FirstOrDefault();
            currentMessage.DeletedDate = DateTime.Now;
            db.Entry(currentMessage).State = EntityState.Modified;
            db.SaveChanges();
            var allMessages = db.ContactUs.OrderByDescending(w => w.Id).Where(w => w.DeletedDate == null).ToList();
            return PartialView("~/Areas/Admin/Views/ContactUS/listofmessage.cshtml", allMessages);
        }
    }
}
