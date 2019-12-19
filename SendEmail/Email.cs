﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Mail;

namespace SendEmail
{
    public class GmailClient
    {
        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="toemail"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public async static Task SendEmailAsync(string toEmail, string subject, string body, string attachment)
        {
            try
            {
                using (MailMessage mail = new MailMessage()) { 
                    mail.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString());
                    mail.To.Add(toEmail);

                    mail.Subject = subject;

                    mail.Body = body;

                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient()) { 
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Email"].ToString(), ConfigurationManager.AppSettings["EmailPassword"].ToString());

                        await smtp.SendMailAsync(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                //msg = "Invalid emailid or password or internet connection is not available";
            }
        }
    }
}
