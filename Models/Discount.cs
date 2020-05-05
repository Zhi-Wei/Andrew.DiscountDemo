using Andrew.DiscountDemo.Rules;

namespace Andrew.DiscountDemo.Models
{
    public class Discount
    {
        public int Id { get; set; }

        public RuleBase Rule { get; set; }

        public Product[] Products { get; set; }

        public decimal Amount { get; set; }
    }
}