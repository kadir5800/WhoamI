using System;
using WhoamI.Core.Data;

namespace  WhoamI.Core.Domain.Entities.Auditing
{
    public interface IHasDeletionTime : ISoftDelete
    {
        DateTimeOffset? DeletionTime { get; set; }
    }
}
