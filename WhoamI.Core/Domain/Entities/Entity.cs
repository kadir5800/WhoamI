namespace  WhoamI.Core.Domain.Entities
{
    public abstract class Entity : IEntity
    {
        public override string ToString()
        {
            string keys = string.Empty;
            foreach (var key in GetKeys())
            {
                keys = string.Concat(keys, ",", key.ToString());
            }

            return $"[ENTITY: {GetType().Name}] Keys = {keys}";
        }

        public abstract object[] GetKeys();
    }

    public abstract class Entity<TKey> : Entity, IEntity<TKey>
    {
        public virtual TKey Id { get; set; }

        protected Entity()
        {

        }

        protected Entity(TKey id)
        {
            Id = id;
        }
    }
}
