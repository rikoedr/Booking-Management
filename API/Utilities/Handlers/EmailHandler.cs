﻿using System.Net.Mail;
using API.Contracts;

namespace API.Utilities.Handlers;

/*
 * Email Handler adalah class untuk melakukan pengiriman pesan,
 * class ini akan dimanfaatkan untuk nantinya mengirimkan kode OTP
 */

public class EmailHandler : IEmailHandler
{
    private string _server;
    private int _port;
    private string _fromEmailAddress;

    public EmailHandler(string server, int port, string fromEmailAddress)
    {
        _server = server;
        _port = port;
        _fromEmailAddress = fromEmailAddress;
    }

    public void Send(string subject, string body, string toEmail)
    {
        MailMessage message = new MailMessage()
        {
            From = new MailAddress(_fromEmailAddress),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        
        message.To.Add(new MailAddress(toEmail));

        using SmtpClient smtpClient = new SmtpClient(_server, _port);
        smtpClient.Send(message);
    }
}