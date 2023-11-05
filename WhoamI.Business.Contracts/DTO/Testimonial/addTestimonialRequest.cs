using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.Testimonial
{
    public class addTestimonialRequest
    {
        public string Name { get; set; }
        public string Opinion { get; set; }
        public string Surname { get; set; }
        public string Job { get; set; }
        public int UserId { get; set; }
    }
}
