using System;

namespace WebAPILogBook.Models
{
    public class WorkLogModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfLog { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}