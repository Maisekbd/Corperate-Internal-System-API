using AAAID.HR.Entities;
using DocumentsExplorer.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DocumentsExplorer.BLL.Helpers
{
    public class Mail
    {
        public static bool SendEmail(NotificationDTO notification, string action, string toMail)
        {
            try
            {
                string body = createEmailBody(notification, action);
                SendHtmlFormattedEmail(notification.Title, body, toMail);
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private static string createEmailBody(NotificationDTO notification, string action)
        {

            string body = string.Empty;
            var templateName = ((EnumNotificationType)notification.NotificationType).ToString("g");
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(String.Format("{0}{1}.html", "~/Content/MailTemplates/", templateName))))
            {
                body = reader.ReadToEnd();
            }
            if (notification.NotificationType == (int)EnumNotificationType.DecisionExecution || notification.NotificationType == (int)EnumNotificationType.DecsionForInform)
            {
                body = body.Replace("{DecisionSubject}", notification.Decision.Subject);
                body = body.Replace("{DecisionLink}", String.Format("{0}/{1}", ConfigurationManager.AppSettings["SystemURl"], "/home/viewDecision/" + notification.Decision.Id));
                body = body.Replace("{DecisionNO}", notification.Decision.Id.ToString());
                body = body.Replace("{DecisionDate}", notification.Decision.ExecutionDate.ToString());
                body = body.Replace("{Action}", action);
            }
            else if (notification.NotificationType == (int)EnumNotificationType.MeetingRequest)
            {
                body = body.Replace("{meetingNo}", notification.Meeting.MeetingNumber.ToString());
                body = body.Replace("{meetingDate}", notification.Meeting.MeetingDate.ToLongDateString());
                body = body.Replace("{meetingTime}", notification.Meeting.MeetingDate.ToShortTimeString());
                body = body.Replace("{DecisionLink}", String.Format("{0}/{1}", ConfigurationManager.AppSettings["SystemURl"], "/home/viewMeeting/" + notification.MeetingId));
            }
            return body;

        }

        private static void SendHtmlFormattedEmail(string subject, string body, string toEmail)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailAddress"]);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(toEmail));
                System.Net.Mail.SmtpClient emailClient = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["MailHost"],Convert.ToInt32(ConfigurationManager.AppSettings["MailPort"]));
                System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUserMailId"], ConfigurationManager.AppSettings["SMTPUserPasssword"]);
                emailClient.UseDefaultCredentials = false;
                emailClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                emailClient.Credentials = SMTPUserInfo;
                emailClient.EnableSsl = false;
                emailClient.Send(mailMessage);


            }

        }

    }
}