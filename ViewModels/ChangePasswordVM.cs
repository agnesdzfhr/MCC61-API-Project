using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCC61_API_Project.ViewModels
{
    public class ChangePasswordVM
    {
        public string Email { get; set; }
        public int OTP { get; set; }
        public string NewPassword { get; set; }
    }
}
