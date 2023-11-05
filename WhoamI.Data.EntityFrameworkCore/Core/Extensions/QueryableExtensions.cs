using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WhoamI.Data.EntityFrameworkCore.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereFullText<T>(this IQueryable<T> query, string property, string text)
        {
            return query.Where(q => EF.Functions.FreeText(property, text));
        }
    }
}
