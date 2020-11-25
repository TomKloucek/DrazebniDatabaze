using System;
using System.Collections.Generic;
using System.Text;

namespace Drazebni_databaze
{
    public interface IValidator<T>
    {
        void Validate(T value);
    }
}
