using System;

namespace TurfCourtsBooking.Models
{
    public class TurfBookingModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public string Starttime { get; set; }
        public string Endtime { get; set; }
        public string Turftype { get; set; }
    }
}