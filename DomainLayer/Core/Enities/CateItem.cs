using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Core.Enities
{
    public class CateItem: BaseEntity
    {
        public int Id { get; set; }

        //making relationship
        [ForeignKey("CateId")]
        public Category category { get; set; }
        public int CateId { get; set; }

        [ForeignKey("ItemId")]
        public Item item { get; set; }
        public int ItemId { get; set; } 
    }
}
