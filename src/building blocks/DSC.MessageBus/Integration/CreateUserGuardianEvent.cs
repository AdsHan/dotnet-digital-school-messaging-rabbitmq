namespace DSC.MessageBus.Integration
{
    public class CreateUserGuardianEvent : Event
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public CreateUserGuardianEvent(string email, string password, string phone)
        {
            Email = email;
            Password = password;
            Phone = phone;
        }
    }
}