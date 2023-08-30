using System.Web.Mvc;
using TurfCourtsBooking.DatabaseConnection;
using TurfCourtsBooking.Models;

namespace SportController
{
    public class SportController : Controller
    {
        DatabaseConnection DatabaseConnection = new DatabaseConnection();

        #region Sports View
        public ActionResult SportsView()
        {
            var sports = DatabaseConnection.SportDataAdd();
            return View(sports);
        }
        #endregion

        #region Sports Add View
        public ActionResult SportsAddView()
        {
            return View();
        }
        #endregion

        #region Sports Add
        [HttpPost]
        public ActionResult SportsAdd(SportModel sport)
        {
            bool result = DatabaseConnection.SportsDataAdd(sport);
            if (result == true)
            {
                TempData["registrationSuccessMessage"] = "Sport added successfully.";
                return RedirectToAction("SportsView");
            }
            else
            {
                TempData["registrationErrorMessage"] = "Sport registration failed!";
                return RedirectToAction("SportsView");
            }
        }
        #endregion

        #region Sports Delete
        public ActionResult SportsDelete(int id)
        {
            var result = DatabaseConnection.SportsDataDelete(id);
            if (result == true)
            {
                TempData["registrationSuccessMessage"] = "Sport Deleted Successfully.";
                return RedirectToAction("SportsView");
            }
            else
            {
                TempData["registrationErrorMessage"] = "Sport Deletion Failed!";
                return RedirectToAction("SportsView");
            }
        }
        #endregion
    }
}