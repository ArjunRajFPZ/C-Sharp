using System.ComponentModel.DataAnnotations;

namespace CRUDWebApplication.Models
{
    public class SPTaskbookModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Assignedfrom { get; set; }
        [Required]
        public string Assignedto { get; set; }
        [Required]
        public DateTime Assigneddate { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
