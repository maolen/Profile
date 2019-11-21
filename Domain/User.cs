namespace Domain
{
    public class User : Entity
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}