using System.Web.Mvc;
using TurfCourtsBooking.DatabaseConnection;
using TurfCourtsBooking.Models;
using TurfCourtsBooking.MultiViewModel;

namespace TurfBookingController
{
    public class TurfBookingController : Controller
    {
        DatabaseConnection DatabaseConnection = new DatabaseConnection();

        #region Venue View
        public ActionResult VenueView()
        {
            var venue = DatabaseConnection.VenueListView();
            var holidayDetail = DatabaseConnection.HolidayDataView();
            TurfViewAndAddUserDetails venueData = new TurfViewAndAddUserDetails()
            {
                venueModel = venue,
                holidayModel = holidayDetail
            };
            return View(venueData);
        }
        #endregion

        #region Turf Booking View
        public ActionResult RegistrationView()
        {
            var userEmail = User.Identity.Name.Split('|')[1];
            var registration = DatabaseConnection.TurfBookingView(userEmail);
            return View(registration);
        }
        #endregion

        #region Turf Booking Admin View
        public ActionResult RegistrationViewAdmin()
        {
            var registration = DatabaseConnection.TurfBookingViewAdmin();
            return View(registration);
        }
        #endregion

        #region Turf Booking Details
        public ActionResult RegistrationAddView(TurfBookingModel booking)
        {
            var userEmail = User.Identity.Name.Split('|')[1];
            var userDetails = DatabaseConnection.UserDetails(userEmail);
            var checkList = DatabaseConnection.CheckSlot(booking);
            var sportsList = DatabaseConnection.CheckSports(booking.Turftype);
            var timeSlot = DatabaseConnection.TimeSlotVenueView();
            TempData["userDate"] = booking.Date;
            TempData["Turftype"] = booking.Turftype;
            TurfViewAndAddUserDetails userdata = new TurfViewAndAddUserDetails()
            {
                userModel = userDetails,
                turfBookingModel = checkList,
                sportModel = sportsList,
                timeSlotModel = timeSlot
            };
            return View(userdata);
        }
        #endregion

        #region Booking Add
        [HttpPost]
        public ActionResult RegistrationAdd(TurfBookingModel booking)
        {
            bool result = DatabaseConnection.TurfBookingAdd(booking);
            if (result == true)
            {
                TempData["registrationSuccessMessage"] = $"Slot booked successful for {booking.Sport} {booking.Turftype} on {booking.Date.ToString("dd/MM/yyyy")}";
                return RedirectToAction("RegistrationView");
            }
            else
            {
                TempData["registrationErrorMessage"] = "Booking failed! Please try again.";
                return RedirectToAction("RegistrationAddView");
            }
        }
        #endregion
    }
}