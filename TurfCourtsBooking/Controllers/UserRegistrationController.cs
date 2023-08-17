using System.Web.Mvc;
using TurfCourtsBooking.DatabaseConnection;
using TurfCourtsBooking.Models;

namespace UserRegistrationController
{
    public class UserRegistrationController : Controller
    {
        DatabaseConnection DatabaseConnection = new DatabaseConnection();

        #region User View
        public ActionResult RegistrationView()
        {
            var users = DatabaseConnection.UserDataView();
            return View(users);
        }
        #endregion

        #region User Add View
        [AllowAnonymous]
        public ActionResult RegistrationAdd()
        {
            return View();
        }
        #endregion

        #region User Add And Email Check
        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegistrationAdd(RegistrationModel user)
        {
            if (ModelState.IsValid)
            {
                var emailCheck = DatabaseConnection.EmailCheck(user);
                if (emailCheck.Email == null)
                {
                    bool result = DatabaseConnection.UserDataAdd(user);
                    if (result == true)
                    {
                        TempData["successRegistrationMessage"] = "Registration successful, login with your user crediantials.";
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        TempData["errorRegistrationMessage"] = "Registration failed ! please try again.";
                        return RedirectToAction("RegistrationAdd");
                    }
                }
                else
                {
                    TempData["errorRegistrationMessage"] = "Email already exists! please try again.";
                    return RedirectToAction("RegistrationAdd");
                }
            }
            return View();
        }
        #endregion
    }
}