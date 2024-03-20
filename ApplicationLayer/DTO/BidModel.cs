using DomainLayer.Core.Enities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    public class BidModel
    {     
        public float BidAmount { get; set; }
    
        public int UserId { get; set; }

        public int ItemId { get; set; }
    }
}
