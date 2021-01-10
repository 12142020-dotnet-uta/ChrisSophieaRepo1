using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models.ViewModels
{
    public class CartVM
    {
        public IDictionary<Item, int> CartDictionary;
        public int StoreId;
    }
}
