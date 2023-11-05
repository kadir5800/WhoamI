using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.ProjectImage
{
    public class addProjectImageRequest
    {
        public IFormFile file { get; set; }
        public int ProjectId { get; set; }
    }
}
