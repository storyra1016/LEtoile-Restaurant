using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp4
{
    public static class CartService
    {
        private static readonly List<CartItem> items = new List<CartItem>();

        public static IReadOnlyList<CartItem> GetItems()
        {
            return items.AsReadOnly();
        }

        public static void AddItem(string name, decimal unitPrice, int quantity = 1)
        {
            if (string.IsNullOrWhiteSpace(name) || unitPrice < 0 || quantity <= 0) return;

            var existing = items.FirstOrDefault(i => string.Equals(i.Name, name, StringComparison.Ordinal));
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                items.Add(new CartItem { Name = name, UnitPrice = unitPrice, Quantity = quantity });
            }
        }

        public static void RemoveItem(string name)
        {
            var existing = items.FirstOrDefault(i => string.Equals(i.Name, name, StringComparison.Ordinal));
            if (existing != null) items.Remove(existing);
        }

        public static void Clear()
        {
            items.Clear();
        }

        public static decimal GetSubtotal()
        {
            return items.Sum(i => i.Subtotal);
        }

        // 計算折扣後總價 multiplier: VIP -> 0.85; 一般 -> 0.95
        public static decimal GetTotalAfterDiscount(bool isVIP)
        {
            var subtotal = GetSubtotal();
            var multiplier = isVIP ? 0.85m : 0.95m;
            return Math.Round(subtotal * multiplier, 2);
        }

        public static decimal GetDiscountAmount(bool isVIP)
        {
            var subtotal = GetSubtotal();
            return Math.Round(subtotal - GetTotalAfterDiscount(isVIP), 2);
        }
    }
}