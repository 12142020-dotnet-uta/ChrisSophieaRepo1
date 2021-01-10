using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models.ViewModels
{
    public class ItemVM
    {
        public Item Item { get; set; }
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }
    }
}
