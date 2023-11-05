using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.ServiceAndHobby
{
    public class updateServiceAndHobbyRequest : addServiceAndHobbyRequest
    {
        public int Id { get; set; }
    }
}
