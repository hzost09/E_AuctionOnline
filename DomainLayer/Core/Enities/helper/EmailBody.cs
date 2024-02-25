using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DomainLayer.Core.Enities.helper
{
    public class EmailBody
    {
        public static string EmailStringBody(string email, string emailToken, IConfiguration configuration)
        {
            string ClientUrl = configuration["ClientUrl"] ?? "https://localhost:4200";

            return $@"
                <head></head>
                <html>
                    <body>
                        <div>
                            <h3>reset</h3>
                            <a href=""{ClientUrl}/reset_password?email={email}&code={emailToken}"" target=""_blank"">ResetPassword</a>    
                        </div>
                    </body>
                </html>";
        }

        public static string Emailverify(string email,string verifyToken, IConfiguration configuration)
        {
            string ClientUrl = configuration["ClientUrl"] ?? "https://localhost:4200";
            return $@"
                <head></head>
                <html>
                    <body>
                        <div>
                            <h3>Verify</h3>
                            <a href=""{ClientUrl}/verify?email={email}&code={verifyToken}"" target=""_blank"">verify password</a>    
                        </div>
                    </body>
                </html>";
        }
    }
}
