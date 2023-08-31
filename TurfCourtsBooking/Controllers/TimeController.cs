using System.Web.Mvc;
using TurfCourtsBooking.DatabaseConnection;
using TurfCourtsBooking.Models;

namespace TimeController
{
    public class TimeController : Controller
    {
        DatabaseConnection DatabaseConnection = new DatabaseConnection();

        #region TimeSlot View
        public ActionResult TimeSlotView()
        {
            var timeSlotList = DatabaseConnection.TimeSlotView();
            return View(timeSlotList);
        }
        #endregion

        #region TimeSlot Edit
        [HttpGet]
        public ActionResult TimeSlotEditView(int id)
        {
            var timeSlot = DatabaseConnection.TimeSlotDataEdit(id);
            return View(timeSlot);
        }
        #endregion

        #region TimeSlot Update
        [HttpPost]
        public ActionResult TimeSlotUpdate(TimeSlotModel timeSlot)
        {
            bool result = DatabaseConnection.TimeSlotDataUpdate(timeSlot);
            if (result == true)
            {
                TempData["registrationSuccessMessage"] = "Venue updated successfully.";
                return RedirectToAction("TimeSlotView");
            }
            else
            {
                TempData["registrationErrorMessage"] = "Venue updation failed!";
                return RedirectToAction("TimeSlotEditView");
            }
        }
        #endregion

        #region TimeSlot Delete
        public ActionResult TimeSlotDelete(int id)
        {
            var result = DatabaseConnection.TimeSlotDataDelete(id);
            if (result == true)
            {
                TempData["deletionSuccessMessage"] = "TimeSlot Deleted Successfully.";
                return RedirectToAction("TimeSlotView");
            }
            else
            {
                TempData["deletionErrorMessage"] = "TimeSlot Deletion Failed!";
                return RedirectToAction("TimeSlotView");
            }
        }
        #endregion
    }
}