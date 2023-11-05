using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.DTO.User;

namespace WhoamI.Business.Contracts.DTO.ServiceAndHobby
{
    public class getAllServiceAndHobbyResponse : dataTableResponse
    {
        public List<getOneServiceAndHobbyResponse> data { get; set; }
    }
}
