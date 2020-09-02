﻿using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class RefreshToken
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual UsersTable User { get; set; }
    }
}
