using System.ComponentModel.DataAnnotations;
using WhoamI.Core.Domain.Entities;

namespace WhoamI.Data.Entitys.Objects
{
    public class UserContact : Entity<int>
    {
        public override object[] GetKeys()
        {
            return new object[] { Id };
        }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string AboutMe { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
