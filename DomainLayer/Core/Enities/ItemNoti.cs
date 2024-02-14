using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Core.Enities
{
    public class ItemNoti:BaseEntity
    {
        public int Id { get; set; }

        //making relationship
        [ForeignKey("ItemId")]                
        public Item Item { get; set; }
        public int ItemId { get; set; }

        [ForeignKey("NotiId")]
        public Notification? Notification { get; set; }  
        public int NotiId { get; set; } 
    }
}
