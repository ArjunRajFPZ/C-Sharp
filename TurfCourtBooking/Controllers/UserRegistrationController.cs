using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TurfCourtBooking.DatabaseConnection;
using TurfCourtBooking.Models;

namespace UserRegistrationController
{
    public class UserRegistrationController : Controller
    {
        DatabaseConnection DatabaseConnection = new DatabaseConnection();

        public ActionResult RegistrationView()
        {
            #region UserView
            var users = DatabaseConnection.UserDataView();
            return View(users); 
            #endregion
        }

        [AllowAnonymous]
        public ActionResult RegistrationAdd()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegistrationAdd(RegistrationModel user)
        {
            #region UserAdd
            if (ModelState.IsValid)
            {
                bool insertData = DatabaseConnection.UserDataAdd(user);
            }
            return View("RegistrationAdd"); 
            #endregion
        }
    }
}
