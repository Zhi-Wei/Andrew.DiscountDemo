using System.Collections.Generic;

namespace Andrew.DiscountDemo.Models
{
    public class CartContext
    {
        public List<Product> PurchasedItems { get; } = new List<Product>();

        public List<Discount> AppliedDiscounts { get; } = new List<Discount>();

        public decimal TotalPrice { get; set; } = 0m;
    }
}