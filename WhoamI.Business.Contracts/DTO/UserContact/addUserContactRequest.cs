using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.UserContact
{
    public class addUserContactRequest
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string AboutMe { get; set; }
        public int UserId { get; set; }
    }
}
