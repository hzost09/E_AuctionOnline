using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DomainLayer.Core.Enities
{
    public class Item: BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float BeginPrice { get; set; }
        public float UpPrice { get; set; }
        public float WinningPrice { get; set; }
        public string? Image { get; set; }
        public string? Document { get; set; }

        [NotMapped]
        [Required]
        public IFormFile? ImageFile { get; set; }

        [NotMapped]
        [Required]
        public IFormFile? DocumentFile { get; set; }

        //making relationship
        [ForeignKey("sellerId")]
        public virtual User? Seller { get; set; }
        public int sellerId { get; set; }
        
        public ICollection<CateItem>? cateItems { get; set; }

        public ICollection<Bid>? bid {  get; set; }  
        
        public Rating? rating { get; set; }

        public AuctionHistory? Auctionhistory { get; set; }

        public ICollection<ItemNoti>? itemNotis { get; set; }
    }
}
