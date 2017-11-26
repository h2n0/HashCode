using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hashCode
{
    class Product
    {
        public int ProductNumb { get; set; }
        public int ProductQuantity { get; set; }
        public Product(int productNumb, int productQuantity) {
            ProductNumb = productNumb;
            ProductQuantity = productQuantity;
        }
    }
}
