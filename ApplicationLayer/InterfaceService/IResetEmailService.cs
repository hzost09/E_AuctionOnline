using ApplicationLayer.DTO;
using DomainLayer.Core;
using DomainLayer.Core.Enities.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.InterfaceService
{
    public interface IResetEmailService
    {
        Task<EmailModel> CheckEmailAndTokenEmail(string email);
        Task<int> checkTokenEmailAndSaveNewPassword(ResetPassWordModel model);
        Task<EmailModel> sendMailForSuccessBuyer(int? buyerId, int? sellerId,string Itemname);
        Task<EmailModel> sendMailForSuccessSeller(int? buyerId, int? sellerId, string Itemname);
        Task<EmailModel> SendEmailtoVerify(VerifyEmail model);
        bool sendMail(EmailModel email);
    }
}
