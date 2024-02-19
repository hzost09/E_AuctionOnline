using DomainLayer.Core.Enities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    public class ItemModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float BeginPrice { get; set; }
        public float UpPrice { get; set; }
        public float WinningPrice { get; set; }
        public string? Email { get; set; }   
        public string? Image { get; set; }
        public string? Document { get; set; }
        public int? sellerId { get; set; }

        public IFormFile? ImageFile { get; set; }

        public IFormFile? DocumentFile { get; set; }
    
        public DateTime? BeginDate { get; set; } 
        public DateTime? EndDate { get; set; }    
    }
}
