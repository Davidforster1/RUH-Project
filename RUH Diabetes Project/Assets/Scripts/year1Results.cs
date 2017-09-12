using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;


public class year1Results : MonoBehaviour {

    private MailMessage mail = new MailMessage(); // Allows for the email to be constructed in the mail function below
    string currentDate = System.DateTime.Now.ToString("hh:mm:ss dd/MM/yyyy");

    [SerializeField]
    public Text resultsText;

    /*[SerializeField]
    public Text emailInput;*/

    private int score;
    private int questionsDone;

    // Use this for initialization - Stores the values 
    void Start () {
        score = year1GameManager.score;
        questionsDone = year1GameManager.questionsDone;
        resultsText.text = "You scored " + score.ToString() + "/" + questionsDone;
    }

    public void yearResetScore() // score resets
    {
        year1GameManager.score = 0;
        year1GameManager.questionsDone = 0;
    }

   /* public void year1SendMail() // Mail send function
        {
              
        string emailAddress; // variable to store user inputted email 
        emailAddress = emailInput.text; // variable becomes the email the user types in
        mail.From = new MailAddress("royalunitedhospitals@gmail.com");
        mail.To.Add(emailAddress);

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        mail.Subject = "CC-EAT Year One: " + currentDate;
        mail.Body = "The patient's answers are listed below:" +
            "\n\n" + 
            "\n Question 2 : Correct";
        smtpServer.Credentials = new System.Net.NetworkCredential("royalunitedhospitals@gmail.com", "Cceat123") as ICredentialsByHost; 
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
        delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; };
        smtpServer.Send(mail);
        Debug.Log("success");
    }*/

}
