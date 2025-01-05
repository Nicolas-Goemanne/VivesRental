using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivesRental.Services.Model.Results
{
    public class AuthenticationResult 
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}
