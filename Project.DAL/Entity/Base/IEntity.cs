using System;

namespace Project.DAL.Entity.Base
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}