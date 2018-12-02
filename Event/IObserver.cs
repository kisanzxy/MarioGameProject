using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahJong.Event
{
    internal interface IObserver<T>
    {
        void subsribe(T subscriber);
    }
}
