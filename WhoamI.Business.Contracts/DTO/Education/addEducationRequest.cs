using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.Education
{
    public class addEducationRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsRunning { get; set; }
        public string Degree { get; set; }
        public string School { get; set; }
        public int UserId { get; set; }
    }
}
