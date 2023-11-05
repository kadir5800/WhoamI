using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Core.Domain.Entities;

namespace WhoamI.Data.Entitys.Objects
{
    public class Portfolio : Entity<int>
    {
        public override object[] GetKeys()
        {
            return new object[] { Id };
        }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string PortfolioType { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
