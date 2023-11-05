using System;

namespace  WhoamI.Core.Domain.Entities.Auditing
{
    public interface IHasModificationTime
    {
        DateTimeOffset? LastModificationTime { get; set; }
    }
}
