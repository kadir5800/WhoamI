using WhoamI.Core.Domain.Entities;

namespace WhoamI.Data.Entitys.Objects
{
    public class ProjectImage : Entity<int>
    {
        public override object[] GetKeys()
        {
            return new object[] { Id };
        }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public string Path { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
