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
            var price = 0d;
            var maxCombineCount = this._carts.Max(d => d.Quantity);

            for (int i = 1; i <= maxCombineCount; i++)
            {
                var combineCarts = this._carts.Where(d => d.Quantity > 0).ToList();
                var discount = GetDiscount(combineCarts.Count);

                foreach (var item in combineCarts)
                {
                    price += (item.Price * 1 * discount);
                    item.Quantity--;
                }
            }
            return price;
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
                case 4:
                    return 0.8;
                case 5:
                    return 0.75;
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
