using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahJong.Controllers
{
    // e.g. move Mario, change Mario to Super Mario
    internal interface ICommand
    {
        // Execute command on the receiver
        void Execute();
    }
}
