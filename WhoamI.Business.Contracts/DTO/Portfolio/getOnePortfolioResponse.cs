using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.Portfolio
{
    public class getOnePortfolioResponse:addPortfolioRequest
    {
        public int Id { get; set; }
        public string CreationDate { get; set; }
    }
}
