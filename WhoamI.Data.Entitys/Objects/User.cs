using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Core.Domain.Entities;

namespace WhoamI.Data.Entitys.Objects
{
    public class User : Entity<int>
    {
        public User()
        {
            this.UserContacts = new List<UserContact>();
            this.Abilities = new List<Ability>();
            this.Articles = new List<Article>();
            this.Educations = new List<Education>();
            this.Experinces = new List<Experince>();
            this.Portfolios = new List<Portfolio>();
            this.Projects = new List<Project>();
            this.ServiceAndHobbies = new List<ServiceAndHobby>();
            this.SocialMedias = new List<SocialMedia>();
            this.Testimonials = new List<Testimonial>();
        }

        public override object[] GetKeys()
        {
            return new object[] { Id };
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<UserContact> UserContacts { get; set; }
        public virtual ICollection<Ability> Abilities { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<Experince> Experinces { get; set; }
        public virtual ICollection<Portfolio> Portfolios { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<ServiceAndHobby> ServiceAndHobbies { get; set; }
        public virtual ICollection<SocialMedia> SocialMedias { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
    }
}
