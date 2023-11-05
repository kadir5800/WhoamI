using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.Testimonial
{
    public class updateTestimonialRequest : addTestimonialRequest
    {
        public int Id { get; set; }
    }
}
