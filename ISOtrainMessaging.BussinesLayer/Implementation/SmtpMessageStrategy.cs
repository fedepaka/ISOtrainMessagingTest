using ISOtrainMessaging.BussinesLayer.Interface;
using ISOtrainMessaging.Model;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ISOtrainMessaging.BussinesLayer.Implementation
{
    public class SmtpMessageStrategy : ISendingStrategy
    {
        public MessageType SupportedChannel => MessageType.Email;
        private readonly SmtpSettings _SmtpSettings;

        public SmtpMessageStrategy(IOptions<NotificationSettings> options)
        {
            _SmtpSettings = options.Value.Smtp;
        }

        /// <summary>
        /// Envío de email por smtp usando MailKit
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<bool> SendAsync(string to, string subject, string body)
        {
            var emailToList = new List<string>() { to };

            //se envía emails
            List<string> reporte = await SendEmail(subject, body, _SmtpSettings.SenderName, _SmtpSettings.SenderEmail, emailToList);

            // 2.Evaluación de resultados
            bool huboError = reporte.Any(r => r.StartsWith("ERROR"));

            if (huboError)
            {
                // Loguear los errores encontrados
                var errores = reporte.Where(r => r.StartsWith("ERROR"));
                foreach (var err in errores)
                {
                    Console.WriteLine($"Fallo al enviar email: {err}");
                }
            }
            else
            {
                // Todo salió bien
                Console.WriteLine("Correo enviado satisfactoriamente");
            }
            return huboError;
        }

        /// <summary>
        /// Envío efectivo de email por SMTP usando MailKit
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="from"></param>
        /// <param name="arrToEmails"></param>
        /// <param name="arrCCEmails"></param>
        /// <param name="arrBCCEmails"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public async Task<List<string>> SendEmail(
            string subject,
            string body,
            string senderName,
            string from,
            List<string> arrToEmails,
            List<string>? arrCCEmails = null,
            List<string>? arrBCCEmails = null,
            List<string>? attachments = null)
        {
            var logs = new List<string>();
            var message = new MimeMessage();

            // 1. Configurar Remitente y Destinatarios
            message.From.Add(new MailboxAddress(senderName, from));

            foreach (string email in arrToEmails)
                message.To.Add(MailboxAddress.Parse(email));

            if (arrCCEmails is not null)
            {
                foreach (string email in arrCCEmails)
                    message.Cc.Add(MailboxAddress.Parse(email));
            }

            if (arrBCCEmails is not null)
            {
                foreach (string email in arrBCCEmails)
                    message.Bcc.Add(MailboxAddress.Parse(email));
            }

            message.Subject = subject;

            // 2. Construir el cuerpo y adjuntos
            var builder = new BodyBuilder { HtmlBody = body };

            if (attachments != null)
            {
                foreach (var filePath in attachments)
                {
                    builder.Attachments.Add(filePath);
                }
            }

            message.Body = builder.ToMessageBody();

            // 3. Envío mediante SmtpClient de MailKit
            using (var client = new SmtpClient())
            {
                try
                {
                    // Nota: Para Docker usa el nombre del servicio o IP de tu servidor SMTP
                    var secureOption = _SmtpSettings.Port == 587 ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;
                    await client.ConnectAsync(_SmtpSettings.Server, _SmtpSettings.Port, secureOption);
                    await client.AuthenticateAsync(_SmtpSettings.Username, _SmtpSettings.Password);
                    // Captura la respuesta
                    string respuesta = await client.SendAsync(message);

                    Console.WriteLine($"El servidor respondió: {respuesta}");

                    // Aquí puedes guardar 'respuesta' en tu objeto Message
                    await client.DisconnectAsync(true);

                    // Log de éxito
                    logs.Add($"EXITO: {respuesta}");
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    Console.WriteLine($"Error enviando mail: {ex.Message}");
                    logs.Add($"ERROR: {ex.Message}");
                }
            }

            return logs;
        }
    }
}
