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

        #region TimeSlot Add View
        public ActionResult TimeSlotAddView()
        {
            return View();
        }
        #endregion

        #region TimeSlot Add
        public ActionResult TimeSlotAdd(TimeSlotModel timeSlot)
        {
            bool result = DatabaseConnection.TimeSlotDataAdd(timeSlot);
            if (result == true)
            {
                TempData["timeSlotSuccessMessage"] = $"Successfully Added Timeslot";
                return RedirectToAction("TimeSlotView");
            }
            else
            {
                TempData["timeSlotErrorMessage"] = "Timeslot adding failed! Please try again.";
                return RedirectToAction("TimeSlotAddView");
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