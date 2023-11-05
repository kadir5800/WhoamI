using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Business.Contracts.DTO.DataTable;

namespace WhoamI.Business.Contracts.DTO.User
{
    public class getAllUserResponse : dataTableResponse
    {
        public List<getOneUserResponse> data { get; set; }
    }
}
