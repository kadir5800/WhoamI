namespace WhoamI.Core.Data
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
