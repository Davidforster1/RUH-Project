using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SendMail : MonoBehaviour
{
    private MailMessage mail = new MailMessage();

    void Start()
    {
        mail.From = new MailAddress("dahidforster@gmail.com");
        mail.To.Add("dahidforster@gmail.com");

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;//GIVE CORRECT PORT HERE
        mail.Subject = "Email test";
        mail.Body = "Testing testing";
        smtpServer.Credentials = new System.Net.NetworkCredential("Royalunitedhospitals@gmail.com", "Cceat123") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
        delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; };
        smtpServer.Send(mail);
        //smtpServer.SendAsync(mail)
        Debug.Log("success");
    }
}