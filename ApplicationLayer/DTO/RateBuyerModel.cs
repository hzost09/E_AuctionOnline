    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    public class RateBuyerModel
    {
        public int ItemId { get; set; }
        public int sellerId { get; set; }

        [Range(0, 5, ErrorMessage = "rate must be between {1} and {2}.")]
        public int RatingAmount { get; set; }
    }
}
