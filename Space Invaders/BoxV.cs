using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid {
    internal class BoxV:BoxView
    {
        public double x, y;

        public BoxV(double x, double y) 
        {
            this.x = x;
            this.y = y;
        }
    }
}
