using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.Experince
{
    public class addExperinceRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsRunning { get; set; }
        public string Company { get; set; }
        public string job { get; set; }
        public int UserId { get; set; }
    }
}
