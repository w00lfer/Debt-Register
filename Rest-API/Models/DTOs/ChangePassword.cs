using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API.Models.DTOs
{
    public class ChangePassword
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
