using System;

namespace Shared.Models
{
    public class UserSharedModel : IUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
    }
}
