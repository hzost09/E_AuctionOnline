using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Core.Enities
{
    public class User:BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; }
        public string Role { get; set; } = "User";
        public string? Avatar { get; set; }
        [NotMapped]
        public IFormFile AvataFile {  get; set; }   
        // making relationship 
        public virtual ICollection<Item>? soldItem { get; set; }

        public ReFreshToken? ReFreshToken { get; set; }

        public EmailToken? EmailToken { get; set; }

        public Rating? Rating { get; set; }  

        public  ICollection<Bid>? bids { get; set; }
    }
}
