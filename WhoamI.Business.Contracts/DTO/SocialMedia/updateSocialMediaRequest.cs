using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.SocialMedia
{
    public class updateSocialMediaRequest : addSocialMediaRequest
    {
        public int Id { get; set; }
    }
}
