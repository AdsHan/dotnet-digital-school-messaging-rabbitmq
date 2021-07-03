using System;
using System.Collections.Generic;

namespace DSC.MessageBus.Integration
{
    public class CreateUserIntegrationEvent : Event
    {

        public Guid Id { get; set; }
        public List<Guardians> Guardians { get; private set; }

        public CreateUserIntegrationEvent()
        {
            Guardians = new List<Guardians>();
        }

        public void AddGuardian(string email, string password, string phone)
        {
            Guardians.Add(new Guardians { Email = email, Password = password, Phone = phone });
        }

    }

    public class Guardians
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }

}