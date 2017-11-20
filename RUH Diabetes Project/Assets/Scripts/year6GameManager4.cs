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

public class year6GameManager4 : MonoBehaviour
{
    public year6Quiz4[] questions;
    private static List<year6Quiz4> unansweredQuestions;
    private year6Quiz4 currentQuestion;

    public static List<string> questionListYear6Part4 = new List<string>(); // questions list
    public static List<string> answerListYear6Part4 = new List<string>(); // answers list
    public static List<string> userSelectionListYear6Part4 = new List<string>(); // user selections list

    private MailMessage mail = new MailMessage(); // Allows for the email to be constructed in the mail function below
    string currentDate = System.DateTime.Now.ToString("HH:mm:ss d/M/yyyy"); // formats date/time into readable format 

    [SerializeField]
    public Text progressText; 

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    Text question;

    [SerializeField]
    public Text userAnswer;

    [SerializeField]
    public RawImage foodImage;

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    [SerializeField]
    RawImage sadSmiley;

    [SerializeField]
    RawImage happySmiley;

    [SerializeField]
    public InputField emailInput; // Where the user types in their email

    [SerializeField]
    private Text emailPlaceholder; // placeholder text in the results scene where user enters email address

    public static int emailTries = 0; // counts how many attempts have been made at emailing

    public static int questionsDoneFour;

    public static int score4;

    private bool beenClicked;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<year6Quiz4>();
        }
        beenClicked = false;
        SetRandomQuestion();
        progressText.text = "Question: " + (questionsDoneFour + 1) + "/" + "6";
    }
    void SetRandomQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        question.text = currentQuestion.foodLabel;
        foodImage.texture = currentQuestion.foodImage;

        questionListYear6Part4.Add(currentQuestion.foodLabel); // populates the list of questions 
        answerListYear6Part4.Add(currentQuestion.userAnswer); // populates the list of correct answers

        unansweredQuestions.RemoveAt(randomQuestionIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDoneFour == 5)
        {
            questionListYear6Part4.ToArray(); // sets all the lists to arrays for email format
            answerListYear6Part4.ToArray();
            userSelectionListYear6Part4.ToArray();
            SceneManager.LoadScene("year6Results"); // if questions done = all of them, load results screen
        }

        questionsDoneFour++;
    }

       public void userSubmitButton()
       {
           if (!beenClicked)
           {
               beenClicked = true;
               userSelectionListYear6Part4.Add(userAnswer.text);
            if (userAnswer.text == currentQuestion.userAnswer)
               {
                   correct.Play(); // plays wrong sound
                   foodImage.texture = happySmiley.texture;
                   score4++;
               }
               else
               {
                   foodImage.texture = sadSmiley.texture;
               }

               StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection
           }
       }

    public void exitEarly()
    {
        questionListYear6Part4.ToArray(); // sets all the lists to arrays for email format
        answerListYear6Part4.ToArray();
        userSelectionListYear6Part4.ToArray();
        SceneManager.LoadScene("year6Results"); // if questions done = all of them, load results screen
    }

    public void year6SendMail() // Mail send function
    {
        if (year1GameManager.storedEmail != "")
        {
            emailInput.text = year1GameManager.storedEmail; // inputtext becomes the stored email as it is not empty
        }
        year1GameManager.emailAddress = emailInput.text; // variable becomes the email the user types in
        mail.From = new MailAddress("royalunitedhospitals@gmail.com");
        mail.To.Add(year1GameManager.emailAddress);
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        mail.Subject = "CC-EAT Year Six: " + currentDate;
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
            "Patient PIN: " + year6GameManager.emailPin + "<br><br>" +
           "The patient's answers are listed below:" + "<br><br>" +
           "<H2> Year 6 Activity 1 </H2>" +
            "<table>" +
            "<tr>" + "<th>" + "Question" + "</th>" + "<th>" + "User Answer" + "</th>" + "<th>" + "Correct Answer" + "</th>" + "</tr>";
        loopThroughArray();
        mail.Body += "</table>" +
            "<H2> Year 6 Activity 2 </H2>" +
            "<table>" +
            "<tr>" + "<th>" + "Question" + "</th>" + "<th>" + "User Answer" + "</th>" + "<th>" + "Correct Answer" + "</th>" + "</tr>";
        loopThroughArray2();
        mail.Body += "</table>" +
            "<H2> Year 6 Activity 3 </H2>" +
            "<table>" +
            "<tr>" + "<th>" + "Question" + "</th>" + "<th>" + "User Answer" + "</th>" + "<th>" + "Correct Answer" + "</th>" + "</tr>";
        loopThroughArray3();
        mail.Body += "</table>" +
            "<H2> Year 6 Activity 4 </H2>" +
            "<table>" +
            "<tr>" + "<th>" + "Question" + "</th>" + "<th>" + "User Answer" + "</th>" + "<th>" + "Correct Answer" + "</th>" + "</tr>";
        loopThroughArray4();
        mail.Body += "</table>" +
         "<br>" + "Total score: " + year6Results.score + "/" + year6Results.questionsDone +
         "<br><br>" + "This was sent from the CC-EAT Diabetes App." + "<br> <br>" +
         "<img src = https://i.imgur.com/AAJY39X.png>";
        smtpServer.Credentials = new NetworkCredential("royalunitedhospitals@gmail.com", "Cceat123") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
        delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; };
        try // tries to send mail
        {
            smtpServer.Send(mail);
            Debug.Log("success");
            year1GameManager.storedEmail = year1GameManager.emailAddress; // only stores email if it sends correctly
            yearResetScore();
            emailTries = 0;
            SceneManager.LoadScene("yearSelection");
        }
        catch (SmtpFailedRecipientsException) // if it fails, reload scene and +1 to tries resulting in error message
        {
            mail.Dispose(); // cancels the mail sending
            emailTries++;
            SceneManager.LoadScene("year6Results2");
        }
    }

    public void loopThroughArray() // loops through array listing each item instead of reusing code
    {
        for (int i = 0; i < year6GameManager.userSelectionListYear6Part1.Count;)
        {
            mail.Body += "<tr>" + "<td>" + year6GameManager.questionListYear6Part1[i] + "</td>" + "<td>" + year6GameManager.userSelectionListYear6Part1[i] + "</td>" + "<td>" + year6GameManager.answerListYear6Part1[i] + "</td>" + "</tr>";
            i++;
        }
    }

    public void loopThroughArray2() // loops through array listing each item instead of reusing code
    {
        for (int i = 0; i < year6GameManager2.userSelectionListYear6Part2.Count;)
        {
            mail.Body += "<tr>" + "<td>" + year6GameManager2.questionListYear6Part2[i] + "</td>" + "<td>" + year6GameManager2.userSelectionListYear6Part2[i] + "g" + "</td>" + "<td>" + year6GameManager2.answerListYear6Part2[i] + "g" + "</td>" + "</tr>";
            i++;
        }
    }

    public void loopThroughArray3() // loops through array listing each item instead of reusing code
    {
        for (int i = 0; i < year6GameManager3.userSelectionListYear6Part3.Count;)
        {
            mail.Body += "<tr>" + "<td>" + year6GameManager3.questionListYear6Part3[i] + "</td>" + "<td>" + year6GameManager3.userSelectionListYear6Part3[i] + "g" + "</td>" + "<td>" + year6GameManager3.answerListYear6Part3[i] + "</td>" + "</tr>";
            i++;
        }
    }

    public void loopThroughArray4() // loops through array listing each item instead of reusing code
    {
        for (int i = 0; i < userSelectionListYear6Part4.Count;)
        {
            mail.Body += "<tr>" + "<td>" + questionListYear6Part4[i] + "</td>" + "<td>" + userSelectionListYear6Part4[i] + "g" + "</td>" + "<td>" + answerListYear6Part4[i] + "g" + "</td>" + "</tr>";
            i++;
        }
    }

    public void offlineMode() // allows the user to exit without sending an email
    {
        yearResetScore();
        emailTries = 0;
        SceneManager.LoadScene("yearSelection");
    }

    public void yearResetScore()
    {
        year6GameManager.score = 0;
        year6GameManager.questionsDone = 0;
        year6GameManager2.score2 = 0;
        year6GameManager2.questionsDoneTwo = 0;
        year6GameManager3.score3 = 0;
        year6GameManager3.questionsDoneThree = 0;
        score4 = 0;
        questionsDoneFour = 0;
        year6GameManager.questionListYear6Part1.Clear();
        year6GameManager.userSelectionListYear6Part1.Clear();
        year6GameManager.answerListYear6Part1.Clear();
        year6GameManager2.questionListYear6Part2.Clear();
        year6GameManager2.userSelectionListYear6Part2.Clear();
        year6GameManager2.answerListYear6Part2.Clear();
        year6GameManager3.questionListYear6Part3.Clear();
        year6GameManager3.userSelectionListYear6Part3.Clear();
        year6GameManager3.answerListYear6Part3.Clear();
        questionListYear6Part4.Clear();
        userSelectionListYear6Part4.Clear();
        answerListYear6Part4.Clear();
    }
}
