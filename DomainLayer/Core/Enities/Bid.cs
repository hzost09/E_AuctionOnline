using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Core.Enities
{
    public class Bid : BaseEntity
    {
        public int Id { get; set; }
        public float BidAmount { get; set; }
        //making relationship
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int UserId { get; set; }

        [ForeignKey("ItemId")]
        public Item? Item { get; set; }
        public int ItemId { get; set; }


    }
}
