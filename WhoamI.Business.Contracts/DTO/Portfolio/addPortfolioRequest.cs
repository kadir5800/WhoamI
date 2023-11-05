using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.Portfolio
{
    public class addPortfolioRequest
    {
        public string Name { get; set; }
        public string PortfolioType { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
