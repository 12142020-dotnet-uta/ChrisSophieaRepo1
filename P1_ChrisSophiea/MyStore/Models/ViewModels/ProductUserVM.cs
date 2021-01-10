using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models.ViewModels
{
    public class ProductUserVM
    {
        public ProductUserVM()
        {
            ProductList = new List<Item>();
        }
        public ApplicationUser ApplicationUser { get; set; }
        public IList<Item> ProductList { get; set; }

        public double PurchaseTotal { get; set; }
    }
}
