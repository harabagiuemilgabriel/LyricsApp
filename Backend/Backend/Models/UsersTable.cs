using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class UsersTable
    {
        public UsersTable()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int ConfirmEmail { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
