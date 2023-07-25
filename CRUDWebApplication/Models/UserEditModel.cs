namespace CRUDWebApplication.Models
{
    public class UserEditModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Dateofbirth { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
