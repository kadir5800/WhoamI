using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.ServiceAndHobby
{
    public class addServiceAndHobbyRequest
    {
        public bool IsService { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
