using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.Article
{
    public class getOneArticleResponse:addArticleRequest
    {
        public int Id { get; set; }
        public string CreationDate { get; set; }
    }
}
