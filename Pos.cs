using System.Collections.Generic;
using System.Linq;
using Andrew.DiscountDemo.Models;
using Andrew.DiscountDemo.Rules;

namespace Andrew.DiscountDemo
{
    public class Pos
    {
        public List<RuleBase> ActivatedRules { get; } = new List<RuleBase>();

        public bool CheckoutProcess(CartContext cart)
        {
            // Reset Cart
            cart.AppliedDiscounts.Clear();

            cart.TotalPrice = cart.PurchasedItems.Select(p => p.Price).Sum();

            foreach (var discounts in this.ActivatedRules
                .Select(rule => rule.Process(cart).ToArray()))
            {
                cart.AppliedDiscounts.AddRange(discounts);
                cart.TotalPrice -= discounts.Select(d => d.Amount).Sum();
            }

            return true;
        }
    }
}