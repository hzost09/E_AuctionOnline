using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Core.Enities
{
    public class EmailToken:BaseEntity
    {
        public int Id { get; set; }
        public string tokenResetPassword {  get; set; }

        //making relationship
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int UserId { get;set; }
    }
}
