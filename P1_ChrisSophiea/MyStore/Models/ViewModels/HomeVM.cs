using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models.ViewModels
{
    public class HomeVM
    {
        public Store Store { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<ItemCategory> ItemCategories { get; set; }
        public IEnumerable<Store> Stores { get; set; }
        public IEnumerable<Inventory> Inventories { get; set; }
    }
}
