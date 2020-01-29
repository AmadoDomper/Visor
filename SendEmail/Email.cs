using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Mail;
using NLog;

namespace SendEmail
{
    public class GmailClient
    {

        private static readonly NLog.Logger Logger = NLog.LogManager.GetLogger("fileLogger");
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
                Logger.Info("Ingresa envio email");
                using (MailMessage mail = new MailMessage()) { 
                    mail.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString());
                    mail.To.Add(toEmail);

                    mail.Subject = subject;

                    mail.Body = body;

                    mail.IsBodyHtml = true;

                    Logger.Info("Agrega credenciales");

                    using (SmtpClient smtp = new SmtpClient()) { 
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Email"].ToString(), ConfigurationManager.AppSettings["EmailPassword"].ToString());

                        Logger.Info("Usuario: {0}, Contraseña: {1}", ConfigurationManager.AppSettings["Email"].ToString(), ConfigurationManager.AppSettings["EmailPassword"].ToString());

                        Logger.Info("Prepara para enviar");

                        await smtp.SendMailAsync(mail);

                        Logger.Info("Envio exitoso");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Algo salio mal!");
                //msg = "Invalid emailid or password or internet connection is not available";
            }
        }
    }
}
