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
            TurfViewAndAddUserDetails venueData = new TurfViewAndAddUserDetails()
            {
                venueModel = venue
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

        #region Turf Booking User Details
        public ActionResult RegistrationAddView(TurfBookingModel booking)
        {
            var userEmail = User.Identity.Name.Split('|')[1];
            var userDetails = DatabaseConnection.UserDetails(userEmail);
            var checkList = DatabaseConnection.CheckTimeSlot(booking);
            TempData["userDate"] = booking.Date;
            TempData["Turftype"] = booking.Turftype;
            TurfViewAndAddUserDetails userdata = new TurfViewAndAddUserDetails()
            {
                userModel = userDetails,
                turfBookingModel = checkList
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
                TempData["registrationSuccessMessage"] = $"Successfully booked from {booking.Starttime} to {booking.Endtime}";
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