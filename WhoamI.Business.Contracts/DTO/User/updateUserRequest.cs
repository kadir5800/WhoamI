using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.User
{
    public class updateUserRequest: addUserRequest
    {
        public int Id { get; set; }
    }
}
