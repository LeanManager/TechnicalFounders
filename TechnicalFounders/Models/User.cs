using System;

namespace TechnicalFounders.Models
{
    public class User
    {
        // Guid
        public string Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
