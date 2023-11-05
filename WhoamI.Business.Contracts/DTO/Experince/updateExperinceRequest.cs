using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.Experince
{
    public class updateExperinceRequest:addExperinceRequest
    {
        public int Id { get; set; }
    }
}
