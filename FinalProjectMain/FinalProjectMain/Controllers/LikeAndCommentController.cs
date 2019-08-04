using FinalProjectMain.AppCode.Filters;
using FinalProjectMain.Models;
using FinalProjectMain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectMain.Controllers
{
    [shoppingFilterAttributeForUser]
    public class LikeAndCommentController : Controller
    {
        // GET: LikeAndComment
        ShoppingDbContext db = new ShoppingDbContext();
        //Like Click
        [AllowAnonymous]
        [HttpPost]
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

        //Comment
        [AllowAnonymous]
        public ActionResult CommentPage(int Id)
        {

            return View(db.comments.OrderByDescending(w => w.Id).Where(w => w.DeletedDate == null && w.ProductId == Id).ToList());
        }
        [AllowAnonymous]
        public ActionResult writeCommentButton(int productId, int? userId, string commentContex)
        {
            var x = Session[SessionKey.User];
            if (x != null)
            {
                if (commentContex == "")
                {
                    return Json("emptyInput", JsonRequestBehavior.AllowGet);
                }
                var findUser = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email.ToString() == x.ToString()).FirstOrDefault();
                var findProduct = db.products.Where(w => w.DeletedDate == null && w.Id == productId).FirstOrDefault();
                userId = findUser.Id;
                var newComment = new Comment();
                newComment.ProductId = findProduct.Id;
                newComment.UserId = findUser.Id;
                newComment.CommentText = commentContex;
                newComment.CreatedDate = DateTime.Now;
                db.comments.Add(newComment);
                db.SaveChanges();
                return PartialView("~/Views/LikeAndComment/CommentPage.cshtml", db.comments.OrderByDescending(w => w.Id).Where(w => w.DeletedDate == null && w.ProductId == productId).ToList());


            }
            else
            {
                return Json("nosession", JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public ActionResult answerMessage(int? productId, int? commentId, string answercomment)
        {
            var x = Session[SessionKey.User];
            if (x != null)
            {
                if (answercomment=="")
                {
                    return Json("emptyInput", JsonRequestBehavior.AllowGet);
                }
                var findUser = db.user.Where(w => w.DeletedDate == null && w.isBlock == false && w.Email.ToString() == x.ToString()).FirstOrDefault();
                var findAdmin = db.Manager.Where(w => w.DeletedDate == null && w.Email.ToString() == x.ToString()).FirstOrDefault();
                var comment = db.comments.Where(w => w.DeletedDate == null && w.Id == commentId).FirstOrDefault();
                var newAnswerComment = new answerComment();
                if (findUser != null)
                {
                    newAnswerComment.UserId = findUser.Id;
                }
                else if (findAdmin != null)
                {
                    newAnswerComment.ManagerId = findAdmin.Id;
                }
                newAnswerComment.CommentId = comment.Id;
                newAnswerComment.answeComment = answercomment;
                newAnswerComment.CreatedDate = DateTime.Now;
                db.AnswerComments.Add(newAnswerComment);
                db.SaveChanges();
                return PartialView("~/Views/LikeAndComment/CommentPage.cshtml", db.comments.OrderByDescending(w => w.Id).Where(w => w.DeletedDate == null && w.ProductId == productId && w.User.isBlock == false).ToList());

            }

            return Json("nosession", JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult deleteComment(int? commentId, int? productId,int? answerCommentId)
        {
            if (commentId!=null)
            {
                var x = Session[SessionKey.User];
                var comment = db.comments.Where(w => w.DeletedDate == null && w.Id == commentId).FirstOrDefault();
                comment.DeletedDate = DateTime.Now;
                db.Entry(comment).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return PartialView("~/Views/LikeAndComment/CommentPage.cshtml", db.comments.Where(w => w.DeletedDate == null && w.User.isBlock == false && w.ProductId == productId).OrderByDescending(w => w.Id).ToList());

            }
            else if (answerCommentId!=null)
            {
                var x = Session[SessionKey.User];
                var answerComment = db.AnswerComments.Where(w => w.DeletedDate == null && w.Id==answerCommentId).FirstOrDefault();
                answerComment.DeletedDate = DateTime.Now;
                db.Entry(answerComment).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return PartialView("~/Views/LikeAndComment/CommentPage.cshtml", db.comments.Where(w => w.DeletedDate == null && w.User.isBlock == false && w.ProductId == productId).OrderByDescending(w => w.Id).ToList());

            }
            return PartialView("~/Views/LikeAndComment/CommentPage.cshtml", db.AnswerComments.Where(w => w.DeletedDate == null && w.User.isBlock == false && w.CommentId == commentId).OrderByDescending(w => w.Id).ToList());

        }

    }

}