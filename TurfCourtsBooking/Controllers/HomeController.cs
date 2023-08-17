using System.Web.Mvc;
using System.Web.Security;
using TurfCourtsBooking.DatabaseConnection;

namespace HomeController
{
    public class HomeController : Controller
    {
        DatabaseConnection DatabaseConnection = new DatabaseConnection();

        #region Index View
        public ActionResult Index()
        {
            return View();
        } 
        #endregion

        #region Login View
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        #endregion

        #region Login Check
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var check = DatabaseConnection.LoginCheck(email, password);
            if (check.Usertype == null)
            {
                TempData["errorLoginMessage"] = "Invalid Login Crediantials!";
                return RedirectToAction("Login");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(check.Usertype + "|" + check.Email, false);
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        #endregion
    }
}