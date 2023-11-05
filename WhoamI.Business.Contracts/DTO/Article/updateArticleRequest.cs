using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.Article
{
    public class updateArticleRequest:addArticleRequest
    {
        public int Id { get; set; }
    }
}
