using System.Web.Mvc;
using TurfCourtsBooking.DatabaseConnection;
using TurfCourtsBooking.Models;

namespace HolidayController
{
    public class HolidayController : Controller
    {
        DatabaseConnection DatabaseConnection = new DatabaseConnection();

        #region Holiday View
        public ActionResult HolidayView()
        {
            var holidayList = DatabaseConnection.HolidayDataView();
            return View(holidayList);
        }
        #endregion     
        
        #region Holiday Add
        [HttpPost]
        public ActionResult HolidayAdd(HolidayModel holiday)
        {
            bool result = DatabaseConnection.HolidayDataAdd(holiday);
            if (result == true)
            {
                TempData["registrationSuccessMessage"] = $"Successfully Added holiday from {holiday.StartDate.ToString("dd/MM/yyyy")} to {holiday.EndDate.ToString("dd/MM/yyyy")}";
                return RedirectToAction("HolidayView");
            }
            else
            {
                TempData["registrationErrorMessage"] = "Holiday adding failed! Please try again.";
                return RedirectToAction("HolidayAddView");
            }
        }
        #endregion

        #region Holiday Add View
        public ActionResult HolidayAddView()
        {
            return View();
        }
        #endregion

        #region Holiday Delete
        public ActionResult HolidayDelete(int id)
        {
            var result = DatabaseConnection.HolidayDataDelete(id);
            if (result == true)
            {
                TempData["deletionSuccessMessage"] = "Holiday Deleted Successfully.";
                return RedirectToAction("HolidayView");
            }
            else
            {
                TempData["deletionErrorMessage"] = "Holiday Deletion Failed!";
                return RedirectToAction("HolidayView");
            }
        }
        #endregion
    }
}