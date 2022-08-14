namespace JWT.Login.Net6.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAdress { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}