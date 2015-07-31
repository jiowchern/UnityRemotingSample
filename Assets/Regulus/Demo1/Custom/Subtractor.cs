using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Custom
{
    class Subtractor : ISubtractor
    {
        Regulus.Remoting.Value<int> ISubtractor.Sub(int num1, int num2)
        {
            return num1 - num2;
        }
    }
}
