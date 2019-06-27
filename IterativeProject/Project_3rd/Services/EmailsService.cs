using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using Project_3rd.Models;
using System.Data.Entity;
using Project_3rd.Repositories;

namespace Project_3rd.Services
{
    public class EmailsService : IEmailsService
    {
        private IUnitOfWork db;

        public EmailsService(IUnitOfWork db)
        {
            this.db = db;
        }

        public void SendEmail(VoucherModel voucher)
        {
            UserModel buyer = db.UsersRepository.GetByID(voucher.userId);
            OfferModel offer = db.OffersRepository.GetByID(voucher.offerId);

            string subject = "Voucher created";
            // ... lazy loading is not working for voucher...
            // I can try two things:
            // make properties non virtual -- not working....
            // include db reference...
            // Build the message
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><head><title>Voucher created</title></head><body>");
            sb.Append("<table><tr><th>Buyer</th><th>Offer</th><th>Price</th><th>Expires date</th></tr>");
            sb.Append("<tr><td>");
            sb.Append(buyer.first_name + " " + buyer.last_name);
            sb.Append("</td><td>");
            sb.Append("Offer " + offer.id);
            sb.Append("</td><td>");
            sb.Append(offer.action_price);
            sb.Append("</td><td>");
            sb.Append(voucher.expirationDate);
            sb.Append("</td></tr></table></body></html>");

            string body = sb.ToString();
            string FromMail = ConfigurationManager.AppSettings["from"];
            string emailTo = voucher.userModel.email;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"]);
            mail.From = new MailAddress(FromMail);
            mail.To.Add(emailTo);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["smtpPort"]);
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["password"]);
            SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["smtpSsl"]);
            SmtpServer.Send(mail);
        }
    }
}