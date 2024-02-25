using DomainLayer.Core.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Core
{
    public class VerifyEmail:BaseEntity
    {   
        public int Id { get; set; } 
        public string Email { get; set; }   
        public string VerifyToken { get; set; }
        public User? User { get; set; }
        public int? UserId { get; set; } 
    }
}
