using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.Portfolio
{
    public class updatePortfolioRequest:addPortfolioRequest
    {
        public int Id { get; set; }
    }
}
