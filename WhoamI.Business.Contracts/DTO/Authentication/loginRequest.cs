using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.Authentication
{
    public class loginRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}
