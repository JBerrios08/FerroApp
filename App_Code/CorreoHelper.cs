using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace FerroApp.App_Code
{
    public static class CorreoHelper
    {
        public static void EnviarTicket(string destinatario, string rutaAdjunto)
        {
            var smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
            var smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            var smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
            var smtpPass = ConfigurationManager.AppSettings["SmtpPassword"];
            var fromAddress = ConfigurationManager.AppSettings["SmtpFrom"];

            using (var cliente = new SmtpClient(smtpHost, smtpPort))
            {
                cliente.EnableSsl = true;
                cliente.Credentials = new NetworkCredential(smtpUser, smtpPass);

                using (var mensaje = new MailMessage(fromAddress, destinatario))
                {
                    mensaje.Subject = "Tu ticket de compra de Ferro Oriente";
                    mensaje.Body = "Gracias por tu compra. Adjuntamos tu ticket en PDF.";
                    if (!string.IsNullOrEmpty(rutaAdjunto))
                    {
                        mensaje.Attachments.Add(new Attachment(rutaAdjunto));
                    }

                    cliente.Send(mensaje);
                }
            }
        }
    }
}
