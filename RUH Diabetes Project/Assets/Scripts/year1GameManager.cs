using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;

public class year1GameManager : MonoBehaviour {

    public year1Quiz[] imagePanel;
    private static List<year1Quiz> unansweredQuestions;
    private year1Quiz currentQuestion;
    public static List<string> questionList = new List<string>(); // questions list
    public static List<bool> answerList = new List<bool>(); // answers list
    public static List<string> userSelectionList = new List<string>(); // user selections list

    private MailMessage mail = new MailMessage(); // Allows for the email to be constructed in the mail function below
    string currentDate = System.DateTime.Now.ToString("HH:mm:ss d/M/yyyy"); // formats date/time into readable format 
    
    [SerializeField]
    public Text scoreText;

    [SerializeField]
    public Text resultsText;

    [SerializeField]
    public Text foodName;

    [SerializeField]
    AudioSource wrong;

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    RawImage questionImage; // The picture of food 

    [SerializeField]
    public InputField emailInput; // Where the user types in their email

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    public static int questionsDone;

    public static int score;

    private bool beenClicked;

    public static int emailTries = 0 ; // counts how many attempts have been made at emailing

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year1Quiz>();
        }
        beenClicked = false;
        SetRandomImage();
    }
    void SetRandomImage()
    {
        int randomImageIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomImageIndex];

        questionImage.texture = currentQuestion.image; // sets the image to the current question image 
        foodName.text = currentQuestion.foodName;

        questionList.Add(currentQuestion.foodName);
        answerList.Add(currentQuestion.isCorrect);

        unansweredQuestions.RemoveAt(randomImageIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDone == 9)
        {
            questionList.ToArray(); // sets all the lists to arrays for email format
            answerList.ToArray();
            userSelectionList.ToArray();
            SceneManager.LoadScene("year1Results"); // if questions done = all of them, load results screen
        }

        questionsDone++;
    }

    public void userSelectTrue()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionList.Add("True");
            // Button click logic here
            if (currentQuestion.isCorrect)
            {
                correct.Play(); // plays wrong sound
                score++;              
            }
            else
            {
                wrong.Play();
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection
        }  
    }

    public void userSelectFalse()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionList.Add("False");

            if (!currentQuestion.isCorrect)
            {
                correct.Play(); // plays wrong sound
                score++;
            }
            else
            {
                wrong.Play();
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void year1SendMail() // Mail send function
    {
        string emailAddress; // variable to store user inputted email 
        emailAddress = emailInput.text; // variable becomes the email the user types in
        mail.From = new MailAddress("royalunitedhospitals@gmail.com");
        mail.To.Add(emailAddress);
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        mail.Subject = "CC-EAT Year One: " + currentDate;
        mail.IsBodyHtml = true; // allows for html
        mail.Body = @"
        <html lang=""en"">
        <head>    
        <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"">
        <style>
            table {
            font-family: arial, sans-serif;
            border-collapse: collapse;
            width: 100%;
                    }
            td, th {
            border: 1px solid #000000;
            text-align: left;
            padding: 8px;
                    }
            td:hover {background-color: #f5f5f5}
            img { width:128px;height:128px;}                 
        </style>
        </Head>" +
           "The patient's answers are listed below:" + "<br><br>" +
            "<table>" +
            "<tr>" + "<th>" + "Question" + "</th>" + "<th>" + "User Answer" + "</th>" + "<th>" + "Correct Answer" + "</th>" + "</tr>";
            loopThroughArray();
        mail.Body += "</table>" +
         "<br>" + "Total score: " + score +
         "<br><br>" + "This was sent from the CC-EAT Diabetes App." + "<br> <br>" +
         "<img src = https://i.imgur.com/AAJY39X.png>";
        smtpServer.Credentials = new NetworkCredential("royalunitedhospitals@gmail.com", "Cceat123") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        //SceneManager.LoadScene("year1Results"); // reloads the scene after user clicks button         
        ServicePointManager.ServerCertificateValidationCallback =
        delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; };
        try // tries to send mail
        {
            smtpServer.Send(mail);
            Debug.Log("success");
            yearResetScore();
            emailTries = 0;
            SceneManager.LoadScene("yearSelection");
        }
        catch (SmtpFailedRecipientsException) // if it fails, reload scene and +1 to tries resulting in error message
        {
            mail.Dispose();
            emailTries++;
            SceneManager.LoadScene("year1Results");
        }
        
    }

    public void loopThroughArray() // loops through array listing each item instead of reusing code
    {
        for (int i = 0; i < 10;)
        {
            mail.Body += "<tr>" + "<td>" + questionList[i] + "</td>" + "<td>" + userSelectionList[i] + "</td>" + "<td>" + answerList[i] + "</td>" + "</tr>";
            i++;
        }
    }

    public void yearResetScore() // score resets
    {
        score = 0;
        questionsDone = 0;
    }
}
