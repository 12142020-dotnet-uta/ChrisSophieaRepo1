using Microsoft.AspNetCore.Mvc.Rendering;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models.ViewModels
{
    public class InventoryVM
    {
        public IEnumerable<Inventory> Inventories { get; set; }
        public IEnumerable<Store> Stores { get; set; }

    }
}
