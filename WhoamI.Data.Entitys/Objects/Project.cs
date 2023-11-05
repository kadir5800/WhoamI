﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Core.Domain.Entities;

namespace WhoamI.Data.Entitys.Objects
{
    public class Project : Entity<int>
    {
        public Project()
        {
            this.projectImages = new List<ProjectImage>();
        }
    
        public override object[] GetKeys()
        {
            return new object[] { Id };
        }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string WebAddress { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<ProjectImage> projectImages { get; set; }
    }
}
