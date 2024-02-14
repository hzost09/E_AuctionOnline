using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Core.Enities
{
    public class Category: BaseEntity
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }

        //making relationship
        public ICollection<CateItem>? cateItems { get; set; }
    }
}
