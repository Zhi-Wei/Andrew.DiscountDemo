using System.Collections.Generic;
using Andrew.DiscountDemo.Models;

namespace Andrew.DiscountDemo.Rules
{
    public class TotalPriceDiscountRule : RuleBase
    {
        private readonly decimal _minDiscountPrice = 0;
        private readonly decimal _discountAmount = 0;

        public TotalPriceDiscountRule(decimal minPrice, decimal discount)
        {
            this._minDiscountPrice = minPrice;
            this._discountAmount = discount;
            this.Name = $"折價券滿 {minPrice} 抵用 {discount}";
            this.Note = "每次交易限用一次";
        }

        public override IEnumerable<Discount> Process(CartContext cart)
        {
            if (cart.TotalPrice > this._minDiscountPrice)
            {
                yield return new Discount
                {
                    Amount = this._discountAmount,
                    Rule = this,
                    Products = cart.PurchasedItems.ToArray()
                };
            }
        }
    }
}