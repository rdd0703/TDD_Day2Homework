using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartLib
{
    public class Cart
    {
        private List<CartItem> _carts;

        public Cart(List<CartItem> carts)
        {
            this._carts = carts;
        }

        public double Calculate()
        {
            var discount = GetDiscount(this._carts.Count);
            return this._carts.Sum(d => d.Price * d.Quantity * discount);
        }

        private double GetDiscount(int count)
        {
            switch (count)
            {
                case 1:
                    return 1;
                case 2:
                    return 0.95;
                case 3:
                    return 0.9;
                default:
                    return 1;
            }
        }
    }

    public class CartItem
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
