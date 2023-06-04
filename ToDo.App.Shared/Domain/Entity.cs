using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.App.Shared.Domain
{
    public abstract class Entity<TId> : IComparable<Entity<TId>>, IEquatable<Entity<TId>> where TId : IComparable<TId>
    {
        public TId Id { get; protected set; }

        public DateTimeOffset CreatedTimeUtc { get; protected set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? ModifiedTimeUtc { get; protected set; }

        public int CompareTo(Entity<TId>? other)
        {
            if ((object)other is null)
            {
                return 1;
            }

            return Id.CompareTo(other!.Id);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            Entity<TId>? entity = obj as Entity<TId>;
            if ((object)entity is not null)
            {
                return Equals(entity);
            }

            return false;
        }

        public bool Equals(Entity<TId>? other)
        {
            if ((object)other is null)
            {
                return false;
            }

            if (Id == null && other!.Id == null)
            {
                return true;
            }

            if (Id == null || other!.Id == null)
            {
                return false;
            }

            return Id.Equals(other!.Id);
        }
    }
}
