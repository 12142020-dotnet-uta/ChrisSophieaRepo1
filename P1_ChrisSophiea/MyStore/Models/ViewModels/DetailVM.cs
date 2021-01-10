using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models.ViewModels
{
    public class DetailVM
    {
        public DetailVM()
        {
            Inventory = new Inventory();
        }
        public Inventory Inventory { get; set; }

        public bool ExistsInCart { get; set; }
    }
}
