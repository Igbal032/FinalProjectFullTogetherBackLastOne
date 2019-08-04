using FinalProjectMain.Models;
using FinalProjectMain.Models.Entity;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProjectMain
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class shoppingExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        public void OnException(ExceptionContext filterContext)
        {
            var area = "";//todo....

            var controller = (filterContext.RouteData.Values["controller"] ?? "").ToString();
            var action = (filterContext.RouteData.Values["action"] ?? "").ToString();

            //səbəbkar erroru tapmaq
            while (filterContext.Exception.InnerException != null)
                filterContext.Exception = filterContext.Exception.InnerException;

            var model = new HandleErrorInfo(filterContext.Exception, controller, action);

            try
            {
                using (var db = new ShoppingDbContext())
                {
                    var entity = new ErrorHistory();
                    if (!string.IsNullOrWhiteSpace(area))
                        entity.AreaName = area;
                    if (!string.IsNullOrWhiteSpace(controller))
                        entity.ControllerName = controller;
                    if (!string.IsNullOrWhiteSpace(action))
                        entity.ActionName = action;

                    if (filterContext.Exception is HttpException)
                        entity.ErrorCode = (filterContext.Exception as HttpException).GetHttpCode();
                    else if (filterContext.Exception is SqlException)
                    {
                        entity.ErrorCode = (filterContext.Exception as SqlException).Number;
                        entity.ErrorMessage = filterContext.Exception.Message;
                    }
                    else
                        entity.ErrorMessage = filterContext.Exception.Message;
                    logger.Fatal(entity.ErrorMessage);
                    db.ErrorHistory.Add(entity);
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                //logger.Fatal(ex)
            }

            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Home/ErrorPage.cshtml",

                //MasterName = "~/Views/Shared/_Layout.cshtml",
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                TempData = filterContext.Controller.TempData
            };

            filterContext.ExceptionHandled = true;
        }

    }
}