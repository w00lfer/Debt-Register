using System;

namespace Rest_API.Models
{
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
