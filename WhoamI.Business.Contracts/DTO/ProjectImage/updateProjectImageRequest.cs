using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.ProjectImage
{
    public class updateProjectImageRequest : addProjectImageRequest
    {
        public int Id { get; set; }
    }
}
