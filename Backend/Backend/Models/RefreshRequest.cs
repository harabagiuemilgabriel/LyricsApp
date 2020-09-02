using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class RefreshRequest
    {
        public string token { get; set; }
        public string refreshToken { get; set; }
    }
}
