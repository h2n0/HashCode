using System;
using hashCode;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hashCode
{
    class Warehouse
    {
        Pos Location { get; }
		int[] Stock;

		Warehouse(Pos location, int numberOfItems) {
            Location = location;
			Stock = new int[numberOfItems];
        }

		public void addStock(int[] stock){
			for (int i = 0; i < stock.Length; i++) {
				this.Stock[i] += stock[i];
			}
		}
    }
}
