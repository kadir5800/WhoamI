using WhoamI.Core.Domain.Entities;

namespace WhoamI.Data.Entitys.Objects
{
    public class Ability : Entity<int>
    {
        public override object[] GetKeys()
        {
            return new object[] { Id };
        }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public int Degree { get; set; }
        public string Name { get; set; }
        public int AbilityType { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
