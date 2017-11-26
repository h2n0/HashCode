using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hashCode
{
    struct Order
    {
        public Pos Location { get; set; }
        public List<Product> ItemsToDeliver { set; get; }
        public bool Completed { get; set; }
        public Order(Pos location, List<Product> itemsToDeliver)
        {
            Location = location;
            ItemsToDeliver = new List<Product>();
            for (int i = 0; i < itemsToDeliver.Count; i++) {
                ItemsToDeliver.Add(itemsToDeliver[i]);
            }
            Completed = false;
        }
    }
}
