﻿using System.Net.Mail;
using Xamalytics.Common;
using Xamalytics.Services.Interface.Common;
using Microsoft.Extensions.Logging;

namespace Xamalytics.Services.Implementation.Common
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task SendAsync(EmailRequest request)
        {
            var emailClient = new SmtpClient("localhost");

            var message = new MailMessage
            {
                From = new MailAddress(request.FromMail),
                Subject = request.Subject,
                Body = request.Body
            };

            foreach (string to in request.ToMail)
            {
                message.To.Add(new MailAddress(to));
            }

            //TODO:EmailService if there was error, try at least three times. 
            try
            {
                await emailClient.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EmailService: Unhandled Exception for Request {@Request}", request);
            }

            _logger.LogWarning($"Sending email to {request.ToMail} from {request.FromMail} with subject {request.Subject}.");

        }
    }
}
