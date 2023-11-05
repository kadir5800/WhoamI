using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Core.Domain.Entities;

namespace WhoamI.Data.Entitys.Objects
{
    public class Admin : Entity<int>
    {
        public override object[] GetKeys()
        {
            return new object[] { Id };
        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
