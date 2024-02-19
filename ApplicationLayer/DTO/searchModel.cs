using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    public class searchModel
    {
        [Required(ErrorMessage = "Item name required")]
        public string ItemName { get; set; }
        [Required(ErrorMessage = "Category required")]
        public string CategoryName { get; set; } 
    }
}
