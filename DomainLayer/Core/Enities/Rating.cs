using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Core.Enities
{
    public class Rating:BaseEntity
    {
        public int  Id { get; set;}

        //making relationship           
        [ForeignKey("ItemId")]
        public Item? Item { get; set;}   
        public int ItemId { get; set; }

        //be rated
        [ForeignKey("SellerId")]
        [InverseProperty("BeingRateds")]
        public User? seller { get; set; }
        public int SellerId { get; set; }

        // rate other
        [ForeignKey("RatedUserId")] 
        [InverseProperty("Rater")]
        public User? RateUser { get; set; }
        public int RateUserId { get; set; }

        public float Rate { get; set; }
        public DateTime RatingDate { get; set; }
    }
}
