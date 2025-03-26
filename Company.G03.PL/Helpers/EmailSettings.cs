using System.Net;
using System.Net.Mail;

namespace Company.G03.PL.Helpers
{
    public static class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("mnelshazly7@gmail.com", "wktyxyvkgotqgspt");
                client.Send("mnelshazly7@gmail.com", email.To, email.Subject, email.Body);

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
    }
}
