using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.DTO.User;

namespace WhoamI.Business.Contracts.DTO.UserContact
{
    public class getAllUserContactResponse : dataTableResponse
    {
        public List<getOneUserContactResponse> data { get; set; }
    }
}
