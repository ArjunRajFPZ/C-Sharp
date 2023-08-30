using System;
using System.Collections.Generic;

namespace TurfCourtsBooking.Models
{
    public class TurfBookingModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public string Sport { get; set; }
        public List<string> SlotId { get; set; }
        public string Slots { get; set; }
        public string Turftype { get; set; }
    }
}