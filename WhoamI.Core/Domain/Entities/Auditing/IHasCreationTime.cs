using System;

namespace WhoamI.Core.Domain.Entities.Auditing
{
    public interface IHasCreationTime
    {
        DateTimeOffset CreationTime { get; set; }
    }
}
