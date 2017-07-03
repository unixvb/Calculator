using SimpleCalculator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleCalculator.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public string AjaxMethod(string login, string operation)
        {
            var db = new ExpressionDataContext();
            DataTable dt = new DataTable();
            Expression expression = new Expression
            {
                Login = login,
                Operations = operation,
                Result = dt.Compute(operation, "").ToString()
            };
            db.Expressions.InsertOnSubmit(expression);
            db.SubmitChanges();
            return expression.Result;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Expressions()
        {
            var db = new ExpressionDataContext();
            return Json(db.Expressions, JsonRequestBehavior.AllowGet);
        }

    }
}