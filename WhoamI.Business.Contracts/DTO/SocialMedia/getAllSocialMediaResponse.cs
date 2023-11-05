using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.DTO.User;

namespace WhoamI.Business.Contracts.DTO.SocialMedia
{
    public class getAllSocialMediaResponse : dataTableResponse
    {
        public List<getOneSocialMediaResponse> data { get; set; }
    }
}
