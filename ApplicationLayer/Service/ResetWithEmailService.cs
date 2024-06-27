using ApplicationLayer.DTO;
using ApplicationLayer.InterfaceService;
using DomainLayer.Core;
using DomainLayer.Core.Enities;
using DomainLayer.Core.Enities.helper;
using DomainLayer.Imterface;
using InfrastructureLayer.AppDbContext;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Service
{
    public class ResetWithEmailService:IResetEmailService
    {
        private readonly IConfiguration _config;
        private readonly DataContext _data;
        private readonly IUnitOfWork _u;
  
        public ResetWithEmailService(IConfiguration config, IUnitOfWork u, DataContext data)
        {
            _config = config;
            _u = u;
            _data = data;
           
        }
        public bool sendMail(EmailModel email)
        {
            var emailmess = new MimeMessage();
            var from = _config["EmailSetting:Mail"];
            var name = _config["EmailSetting:DisplayName"];
            emailmess.From.Add(new MailboxAddress(name, from));
            emailmess.To.Add(new MailboxAddress(email.To, email.To));
            emailmess.Subject = email.Subject;
            emailmess.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(email.Content)
            };
            using (var client = new SmtpClient())
            {
                try
                {
                    //với cổng 587 => smtp.ethereal.email
                    //client.Connect(_config["EmailSetting:SmtpServe"], 587, SecureSocketOptions.StartTls);
                    client.Connect(_config["EmailSetting:Host"], 587, SecureSocketOptions.StartTls);
                    client.Authenticate(_config["EmailSetting:Mail"], _config["EmailSetting:Password"]);
                    client.Send(emailmess);
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }

        }

        public async Task<EmailModel> CheckEmailAndTokenEmail(string email)
        {
            try
            {
               var takeUser = await _u.Repository<User>().EntitiesCondition().Include(x => x.EmailToken).FirstOrDefaultAsync(x => x.Email == email);    

                if (takeUser == null)
                {
                    return null;
                }
                var tokenByte = RandomNumberGenerator.GetBytes(64);
                var emailToken = Convert.ToBase64String(tokenByte);
                var checkEmailTokenExit = await _u.Repository<EmailToken>().EntitiesCondition().FirstOrDefaultAsync(x => x.UserId == takeUser.Id);
                if (checkEmailTokenExit == null)
                {
                    var newEmailToken = new EmailToken();
                    newEmailToken.tokenResetPassword = emailToken;
                    newEmailToken.UserId = takeUser.Id;
                    newEmailToken.EndDate = DateTime.Now.AddMinutes(15);
                    await _u.Repository<EmailToken>().Create(newEmailToken);
                }
                else
                {
                    checkEmailTokenExit.tokenResetPassword = emailToken;
                    checkEmailTokenExit.EndDate = DateTime.Now.AddMinutes(15);
                    _u.Repository<EmailToken>().UpDate(checkEmailTokenExit);
                }              
                await _u.SaveChangesAsync();
                var bodyemail = new EmailModel(email, "Reset Password", EmailBody.EmailStringBody(email, emailToken, _config));
                return bodyemail;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public async Task<int> checkTokenEmailAndSaveNewPassword(ResetPassWordModel model)
        {
            try
            {
                if (model.ConfirmPassWord == null || model.PasswordReset == null || model.EmailToken == null || model.Email == null)
                {
                    return 0;
                }
                var newToken = model.EmailToken.Replace(" ", "+");
                var user = await _u.Repository<User>().EntitiesCondition().Include(x => x.EmailToken).FirstOrDefaultAsync(x => x.Email == model.Email);
                if (newToken != user.EmailToken.tokenResetPassword || user.EmailToken.EndDate < DateTime.Now)
                {
                    return -1;
                }
                user.Password = model.PasswordReset;
                _data.Attach(user);
                _data.Entry(user).State = EntityState.Modified;
                _u.Repository<User>().UpDate(user);
                await _u.SaveChangesAsync();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        //send mail for user and seller
        public async Task<EmailModel> sendMailForSuccessBuyer(int? buyerId, int? sellerId,string itemName)
        {
            try
            {
               
                var Buyer = await _u.Repository<User>().GetById(buyerId);
                var seller = await _u.Repository<User>().GetById(sellerId);

                var bodyemail = new EmailModel(Buyer.Email, "success trade", successMail.EmailForByer(Buyer.Email, seller.Email,itemName));
                return bodyemail;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<EmailModel> sendMailForSuccessSeller( int? buyerId,int? sellerId,string itemName)
        {
            try
            {
                var Buyer = await _u.Repository<User>().GetById(buyerId);
                var seller = await _u.Repository<User>().GetById(sellerId);
                var bodyemail = new EmailModel(seller.Email, "success trade", successMail.EmailForSeller(Buyer.Email, seller.Email, itemName));
                return bodyemail;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<EmailModel> SendEmailtoVerify(VerifyEmail model)
        {
            try
            {
                var bodyEmail = new EmailModel(model.Email,"verify", EmailBody.Emailverify(model.Email, model.VerifyToken,_config));
                return bodyEmail;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
