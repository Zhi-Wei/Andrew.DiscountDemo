using System.Collections.Generic;
using Andrew.DiscountDemo.Models;

namespace Andrew.DiscountDemo.Rules
{
    public abstract class RuleBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public abstract IEnumerable<Discount> Process(CartContext cart);
    }
}