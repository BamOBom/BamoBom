using System;
namespace Entities
{
    public interface IEntity
    {
    }

    public abstract class BaseEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
