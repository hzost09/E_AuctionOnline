using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    public class SellItemRequest
    {
        public ItemModel ItemModel { get; set; }
        
        public int[] CategoryModel { get; set; }
    }
}
