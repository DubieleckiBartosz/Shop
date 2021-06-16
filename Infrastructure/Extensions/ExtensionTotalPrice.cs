using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Extensions
{
    public static class ExtensionTotalPrice
    {
        public static  decimal TotalPrice(this IEnumerable<OrderLine> orderLines, IEnumerable<Product> products)
        {
            decimal total = 0;
            foreach (var item in orderLines)
            {
                var p=products.Where(c => c.Id == item.ProductId).FirstOrDefault();
                total += p?.Price * item.Quantity ?? 0;
            }return total;
        }
    }
}
