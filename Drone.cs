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
        public int TimeToPosition(Pos target)
        {
            return (int) Math.Ceiling(Math.Sqrt(Math.Pow(Location.X - target.X, 2) + Math.Pow(Location.Y - target.Y, 2)));
        }
    }
}
