using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class UserWithToken:UsersTable
    {
        public string token { get; set; }
        public string refreshToken { get; set; }
        public UserWithToken(UsersTable user)
        {
            this.Id = user.Id;
            this.UserName = user.UserName;
            this.Email = user.Email;
            this.ConfirmEmail = user.ConfirmEmail;
            this.Password = user.Password;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
        }
    }
}
