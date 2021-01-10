using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models.ViewModels
{
    public class CreateInventoryVM
    {
        public Inventory Inventory { get; set; }
        public IEnumerable<SelectListItem> StoreSelectList { get; set; }
        public IEnumerable<SelectListItem> ItemSelectList { get; set; }
    }
}
