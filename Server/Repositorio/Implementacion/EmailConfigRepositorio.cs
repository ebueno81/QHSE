﻿using MailKit.Net.Smtp;
using MimeKit;
using QHSE.Client.Servicios.Contrato;
using QHSE.Server.Repositorio.Contrato;
using QHSE.Server.Utilidades;

namespace QHSE.Server.Repositorio.Implementacion
{
    public class EmailConfigRepositorio : IEMailSenderRepositorio
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailConfigRepositorio(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);

            emailMessage.Subject = message.Subject;

            //envio de texto plano
            // emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("{0}", message.Content) };

            return emailMessage;

        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

    }
}
