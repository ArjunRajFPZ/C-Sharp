using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Security;
using TurfCourtBooking.DatabaseConnection;

namespace HomeController
{
    public class HomeController : Controller
    {
        DatabaseConnection DatabaseConnection = new DatabaseConnection();

        
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            #region Login
            var check = DatabaseConnection.LoginCheck(email, password);
            if (check.Usertype == null)
            {
                TempData["errorMessage"] = "Invalid Login Crediantials!";
                return RedirectToAction("Login");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(check.Usertype, false);
                return RedirectToAction("Index");
            }
            #endregion
        }

        public ActionResult Logout()
        {
            #region Logout
            FormsAuthentication.SignOut();
            return RedirectToAction("Login"); 
            #endregion
        }
    }
}