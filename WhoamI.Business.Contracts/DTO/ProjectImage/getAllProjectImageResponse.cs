using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.DTO.User;

namespace WhoamI.Business.Contracts.DTO.ProjectImage
{
    public class getAllProjectImageResponse : dataTableResponse
    {
        public List<getOneProjectImageResponse> data { get; set; }
    }
}
