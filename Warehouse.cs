using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hashCode
{
    class Warehouse
    {
        Pos Location { get; }
        Warehouse(Pos location) {
            Location = location;
        }
    }
}
