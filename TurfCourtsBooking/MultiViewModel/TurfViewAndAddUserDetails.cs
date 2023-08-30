using System.Collections.Generic;
using TurfCourtsBooking.Models;

namespace TurfCourtsBooking.MultiViewModel
{
    public class TurfViewAndAddUserDetails
    {
        public RegistrationModel userModel { get; set; }
        public List<TurfBookingModel> turfBookingModel { get; set; }
        public List<VenueModel> venueModel { get; set; }
        public List<HolidayModel> holidayModel { get; set; }
        public List<SportModel> sportModel { get; set; }
        public List<TimeSlotModel> timeSlotModel { get; set; }

    }
}