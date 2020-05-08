using System;
using System.Collections.Generic;
using System.IO;
using Andrew.DiscountDemo.Models;
using Andrew.DiscountDemo.Rules;
using Newtonsoft.Json;

namespace Andrew.DiscountDemo
{
    internal class Program
    {
        private static int _seed = 0;

        private static void Main(string[] args)
        {
            // 購物車 Context。
            var cart = new CartContext();
            // POS。
            var pos = new Pos();

            // 購物車加入購買商品。
            cart.PurchasedItems.AddRange(LoadProducts());
            // POS 加入啟用的折扣規則。
            pos.ActivatedRules.AddRange(LoadRules());

            // 對購物車 Context 進行結帳。
            pos.CheckoutProcess(cart);

            // 列印收據。
            PrintReceipt(cart);

            Console.Read();
        }

        private static IEnumerable<Product> LoadProducts(string filename = @"products.json")
        {
            foreach (var p in JsonConvert.DeserializeObject<Product[]>(File.ReadAllText(filename)))
            {
                _seed++;
                p.Id = _seed;
                yield return p;
            }
        }

        private static IEnumerable<RuleBase> LoadRules()
        {
            yield return new BuyMoreBoxesDiscountRule(2, 12);   // 買 2 箱，折扣 12%
            yield return new TotalPriceDiscountRule(1000, 100); // 滿 1000 折 100
        }

        private static void PrintReceipt(CartContext cart)
        {
            Console.WriteLine("購買商品：");
            Console.WriteLine("---------------------------------------------------");

            foreach (var p in cart.PurchasedItems)
            {
                Console.WriteLine($"- {p.Id,02}, [{p.Sku}] {p.Price,8:C}, {p.Name} {p.TagsValue}");
            }

            Console.WriteLine();
            Console.WriteLine("折扣：");
            Console.WriteLine("---------------------------------------------------");

            foreach (var d in cart.AppliedDiscounts)
            {
                Console.WriteLine($"- 折抵 {d.Amount,8:C}, {d.Rule.Name} ({d.Rule.Note})");

                foreach (var p in d.Products)
                {
                    Console.WriteLine($"  * 符合: {p.Id,02}, [{p.Sku}], {p.Name} {p.TagsValue}");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine($"結帳金額：   {cart.TotalPrice:C}");
        }
    }
}