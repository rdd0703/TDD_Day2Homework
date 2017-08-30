﻿using System;
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
            var lstPrice = new List<int>();

            //平均組合計算法
            lstPrice.Add(GetAvgCombinePrice(this._carts));
            //一般依序計算法
            lstPrice.Add(GetNormalPrice(this._carts));


            return lstPrice.Min();
        }

        /// <summary>一般依序計算法</summary>
        private int GetNormalPrice(List<CartItem> carts)
        {
            var price = 0;
            var maxCombineCount = carts.Max(d => d.Quantity);

            for (int i = 1; i <= maxCombineCount; i++)
            {
                var combineCarts = carts.Where(d => d.Quantity > 0).ToList();
                var discount = GetDiscount(combineCarts.Count);

                foreach (var item in combineCarts)
                {
                    price += (int)(item.Price * 1 * discount);
                    item.Quantity--;
                }
            }
            return price;
        }

        /// <summary>平均組合計算法</summary>
        private int GetAvgCombinePrice(List<CartItem> carts)
        {
            var price = 0;
            var maxCombineCount = (double)carts.Max(d => d.Quantity);

            var lstPrice = new List<double>();
            foreach (var item in carts)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    lstPrice.Add(item.Price);
                }
            }

            var oneCombineCount = (int)Math.Ceiling(lstPrice.Count / maxCombineCount);
            for (int i = 0; i < maxCombineCount; i++)
            {
                var discount = GetDiscount(oneCombineCount);
                price += (int)lstPrice.Skip(i * oneCombineCount).Take(oneCombineCount).Sum(d => d * discount);

                if (i == maxCombineCount - 2)
                    oneCombineCount = lstPrice.Count - ((i + 1) * oneCombineCount);
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
