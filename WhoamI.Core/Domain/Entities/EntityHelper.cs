using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace  WhoamI.Core.Domain.Entities
{
    public static class EntityHelper
    {
        public static bool IsEntity(Type type)
        {
            return typeof(IEntity).IsAssignableFrom(type);
        }

        public static bool HasDefaultId<TKey>(IEntity<TKey> entity)
        {
            if (EqualityComparer<TKey>.Default.Equals(entity.Id, default))
            {
                return true;
            }

            if (typeof(TKey) == typeof(int))
            {
                return Convert.ToInt32(entity.Id) <= 0;
            }

            if (typeof(TKey) == typeof(long))
            {
                return Convert.ToInt64(entity.Id) <= 0;
            }

            return false;
        }

        public static Type FindPrimaryKeyType<TEntity>()
            where TEntity : IEntity
        {
            return FindPrimaryKeyType(typeof(TEntity));
        }

        public static Type FindPrimaryKeyType(Type entityType)
        {
            if (!typeof(IEntity).IsAssignableFrom(entityType))
            {
                throw new Exception($"Given {nameof(entityType)} is not an entity. It should implement {typeof(IEntity).AssemblyQualifiedName}!");
            }

            foreach (var interfaceType in entityType.GetTypeInfo().GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
            }

            return null;
        }

        public static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId<TEntity, TKey>(TKey id)
            where TEntity : IEntity<TKey>
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, nameof(Entity<TKey>.Id)),
                Expression.Constant(id, typeof(TKey))
            );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}