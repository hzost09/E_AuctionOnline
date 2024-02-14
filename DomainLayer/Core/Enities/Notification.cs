using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Core.Enities
{
    public class Notification:BaseEntity
    {
        public int Id { get; set; } 
        public string Type { get; set; }
        
        //making relationship
        public ICollection<ItemNoti>? ItemNoti { get; set; }
    }
}
