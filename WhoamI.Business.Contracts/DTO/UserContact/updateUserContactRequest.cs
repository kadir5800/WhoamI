using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.UserContact
{
    public class updateUserContactRequest : addUserContactRequest
    {
        public int Id { get; set; }
    }
}
