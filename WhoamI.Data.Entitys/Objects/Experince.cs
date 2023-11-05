using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Core.Domain.Entities;

namespace WhoamI.Data.Entitys.Objects
{
    public class Experince : Entity<int>
    {
        public override object[] GetKeys()
        {
            return new object[] { Id };
        }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsRunning { get; set; }
        public string Company { get; set; }
        public string job { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
