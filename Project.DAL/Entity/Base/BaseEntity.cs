using System;

namespace Project.DAL.Entity.Base
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}