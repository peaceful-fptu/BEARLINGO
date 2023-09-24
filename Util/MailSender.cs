using System.Net.Mail;
using System.Net;

namespace BEARLINGO.Util
{
    public class MailSender
    {
        public MailSender() { }

        public static void SendMail(string toAddress, string subject, string body)
        {
            try
            {
                var fromAddress = new MailAddress("bearlingo98@gmail.com");
                var receiver = new MailAddress(toAddress);
                const string fromPass = "njec jxuo moyv pjbg";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPass),
                    Timeout = 200000
                };
                using (var message = new MailMessage(fromAddress, receiver)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                Console.WriteLine("Đã gửi thành công");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
