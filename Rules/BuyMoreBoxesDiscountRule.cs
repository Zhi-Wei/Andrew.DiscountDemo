using System.Collections.Generic;
using System.Linq;
using Andrew.DiscountDemo.Models;

namespace Andrew.DiscountDemo.Rules
{
    public class BuyMoreBoxesDiscountRule : RuleBase
    {
        private readonly int _boxCount = 0;
        private readonly int _percentOff = 0;

        public BuyMoreBoxesDiscountRule(int boxes, int percentOff)
        {
            this._boxCount = boxes;
            this._percentOff = percentOff;
            this.Name = $"任 {this._boxCount} 箱結帳 {100 - this._percentOff} 折！";
            this.Note = "熱銷飲品 限時優惠";
        }

        public override IEnumerable<Discount> Process(CartContext cart)
        {
            var matchedProducts = new List<Product>();

            foreach (var p in cart.PurchasedItems)
            {
                matchedProducts.Add(p);

                if (matchedProducts.Count != this._boxCount)
                {
                    continue;
                }

                // 符合折扣
                yield return new Discount
                {
                    Amount = matchedProducts.Select(product => product.Price).Sum() * this._percentOff / 100,
                    Products = matchedProducts.ToArray(),
                    Rule = this,
                };

                matchedProducts.Clear();
            }
        }
    }
}