using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.ProjectImage
{
    public class getOneProjectImageResponse 
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Path { get; set; }
        public string CreationDate { get; set; }
    }
}
