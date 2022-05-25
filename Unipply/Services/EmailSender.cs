using System;
using System.Net;
using System.Net.Mail;
using Unipply.Models;

namespace Unipply.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender()
        {
        }

        public void Send(EmailModel model) 
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("tanyatsy00@gmail.com", "karapuznan6"),
                    EnableSsl = true
                };

                try
                {

                    client.Send(model.From, model.To, model.Subject, model.Body);

                }
                catch (SmtpFailedRecipientsException ex)
                {
                    for (int i = 0; i < ex.InnerExceptions.Length; i++)
                    {
                        SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                        if (status == SmtpStatusCode.MailboxBusy ||
                            status == SmtpStatusCode.MailboxUnavailable)
                        {
                            Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                            System.Threading.Thread.Sleep(5000);
                            client.Send(model.From, model.To, model.Subject, model.Body);
                        }
                        else
                        {
                            Console.WriteLine("Failed to deliver message to {0}",
                                ex.InnerExceptions[i].FailedRecipient);
                        }
                    }
                }
            }
            catch (Exception) { }
        }
    }

    public interface IEmailSender
    {
        void Send(EmailModel model);
    }
}
