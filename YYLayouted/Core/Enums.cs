using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYLayouted
{
    public enum ControlStates
    {
        Normal = 0,
        Hover = 1,
        Pressed = 2,
    }

    public enum MouseOperationType
    {
        Move,
        Down,
        Up,
        Hover,
        Leave,
        Wheel
    }

}
