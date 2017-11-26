using System;
using hashCode;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hashCode
{
    class Drone
    {
        Pos Location { get; set; }
        Drone(){
            Location = new Pos(0, 0);
        }
    }
}
