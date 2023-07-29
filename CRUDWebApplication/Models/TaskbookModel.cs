namespace CRUDWebApplication.Models
{
    public class TaskbookModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Assignedfrom { get; set; }
        public string Assignedto { get; set; }
        public DateTime Assigneddate { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
    }
}
