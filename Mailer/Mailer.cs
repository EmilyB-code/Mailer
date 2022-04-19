using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Mailer    
{
    public class Mailer
    {
        public static async Task<int> SendEmail(string sender, string to, string addr, string subject, string body)
        {
            
            //Import credentials from appsettings.json
            string url, port, username, password;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            url = configuration["url"];
            port = configuration["port"];
            username = configuration["username"];
            password = configuration["password"];

            string status = "failed";

            var message = new MimeMessage();

                message.From.Add(new MailboxAddress(sender, username)); //the second field is never used, but displays weird in my inbox if left empty
                message.To.Add(new MailboxAddress(to, addr));
                message.Subject = subject;
                message.Body = new TextPart("plain") { Text = body };

                using (var client = new SmtpClient())
                {
                    client.Connect(url, int.Parse(port));
                    client.Authenticate(username, password);


                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {
                            //Console.WriteLine("sending");
                            status = client.Send(message);
                            //Console.WriteLine(status);
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    using StreamWriter file = new("log.txt", append: true);
                string logEntry = DateTime.Now.ToString() + "\t"
                    + sender + ", " + username + "\t"
                    + to + ", " + addr + "\t"
                    + subject + "\t"
                    + body + "\t"
                    + status + "\n";
                await file.WriteLineAsync(logEntry);

                    client.Disconnect(true);
                }
            return 0;  
        }
    }
}