using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.SocialMedia
{
    public class addSocialMediaRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public int UserId { get; set; }
    }
}
