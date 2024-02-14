using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Core.Enities
{
    public class AuctionHistory : BaseEntity
    {
        public int Id { get; set; }
        public float HighestBid { get; set; }

        //making relationship
        [ForeignKey("ItemId")]
        public Item? Item { get; set; }
        public int ItemId { get; set; }

        [ForeignKey("WinnerId")]
        public User? Winner { get; set; }    
        public int WinnerId { get; set; }
      
    }
}
