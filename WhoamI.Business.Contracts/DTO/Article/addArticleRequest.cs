using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.Article
{
    public class addArticleRequest
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
