using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivesRental.Services.Model.Requests
{
    public class UserRegisterRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
