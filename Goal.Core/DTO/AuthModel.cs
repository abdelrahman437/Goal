using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goal.Core.DTO
{
    public class AuthModel
    {
        public string Massage { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Token { get; set; }
        public DateTime ExpireOn { get; set; }
    }
}
