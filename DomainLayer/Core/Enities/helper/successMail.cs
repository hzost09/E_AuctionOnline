﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Core.Enities.helper
{
    public class successMail
    {
        public static string EmailForSeller(string checkEmailbuyer, string checkEmailseller,string itemname)
        {
            return $@"<html>
        <head></head>
        <body>
        <div>
           Congratulations  {checkEmailseller} , we have the winner for item {itemname}
            is {checkEmailbuyer} please contact with the winner for shipping and transction plan
        </div>
        </body>
        </html>";
        }

        public static string EmailForByer(string checkEmailbuyer, string checkEmailseller,string Itemname)
        {
            return $@"<html>
        <head></head>
        <body>
        <div>
          Congratulations  {checkEmailbuyer}  you are the winner of item {Itemname}, 
           please contact with the seller {checkEmailseller}  for shipping and transction plan
                      
        </div>
        </body>
        </html>";
        }
    }
}
