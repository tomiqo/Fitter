using System;
using System.ComponentModel.DataAnnotations;

namespace Fitter.BL.Model
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
    }
}