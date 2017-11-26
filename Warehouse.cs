using System;
using hashCode;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hashCode
{
    struct Warehouse
    {
        public Pos Location { get; }
		List<Product> Stock;

		public Warehouse(List<Product> numberOfItems, Pos location) {
            Location = location;
            Stock = new List<Product>();
        }
    }
}
