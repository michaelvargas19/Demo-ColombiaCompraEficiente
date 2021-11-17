using Autenticacion.Infraestructura.Email.DTO;
using Autenticacion.Infraestructura.Email.IIntegracion;
using Autenticacion.Infraestructura.Email.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Infraestructura.Email.Integracion
{
    public class ConsumidorEmail : IConsumidorEmail
    {

        private readonly SMTP_Settings _smtpSettings;

        public ConsumidorEmail(IOptions<SMTP_Settings> smtpSettings)
        {
            this._smtpSettings = smtpSettings.Value;
        }


        public async Task EnviarEmail(MailRequest mail)
        {
            try
            {

                var mensaje = new MimeMessage();

                mensaje.From.Add( new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));

                foreach (KeyValuePair<string, string> destinatario in mail.Destinatarios)
                {
                    mensaje.To.Add(new MailboxAddress(destinatario.Key, destinatario.Value));
                }
                                
                mensaje.Subject = mail.Asunto;
                mensaje.Body = new TextPart("html") { Text= mail.Body };


                //using (var cliente = new SmtpClient())
                //{
                //    await cliente.ConnectAsync(_smtpSettings.Server);
                //    await cliente.AuthenticateAsync(_smtpSettings.UserName, _smtpSettings.Password);
                //    await cliente.SendAsync(mensaje);
                //    await cliente.DisconnectAsync(true);
                //}

            }
            catch (Exception e)
            {

            }

        }
    }
}
