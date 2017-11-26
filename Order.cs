using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hashCode
{
    struct Order
    {
        Pos Location { get; set; }
        int[] ItemsToDeliver { get; }
        public Order(Pos location, int[] itemsToDeliver)
        {
            Location = location;
            ItemsToDeliver = itemsToDeliver;
        }
    }
}
