using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Interfaces
{
    public interface IId<T>
    {
        T Id { get; set; }
    }
}
