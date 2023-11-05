using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.Project
{
    public class addProjectRequest
    {
        public string Name { get; set; }
        public string WebAddress { get; set; }
        public IFormFile file { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
