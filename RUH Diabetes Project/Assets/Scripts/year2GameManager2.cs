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

public class year2GameManager2 : MonoBehaviour
{

    public year2Quiz2[] imagePanel;
    private static List<year2Quiz2> unansweredQuestions;
    private year2Quiz2 currentQuestion;

    public static List<string> questionListYear2Part2= new List<string>(); // questions list
    public static List<string> answerListYear2Part2 = new List<string>(); // answers list
    public static List<string> userSelectionListYear2Part2 = new List<string>(); // user selections list

    private MailMessage mail = new MailMessage(); // Allows for the email to be constructed in the mail function below
    string currentDate = System.DateTime.Now.ToString("HH:mm:ss d/M/yyyy"); // formats date/time into readable format 

    [SerializeField]
    public GameObject helpCanvas;

    [SerializeField]
    public Text progressText;

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    RawImage answer1; // The picture of food 

    [SerializeField]
    RawImage answer2; // The picture of food 

    [SerializeField]
    RawImage answer3; // The picture of food 

    [SerializeField]
    RawImage answer4; // The picture of food 

    [SerializeField]
    Text foodnameOne;

    [SerializeField]
    Text foodnameTwo;

    [SerializeField]
    Text foodnameThree;

    [SerializeField]
    Text foodnameFour;

    [SerializeField]
    Text question;

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

    public static int questionsDoneTwo;

    public static int score2;

    private bool beenClicked;

    public static int emailTries = 0; // counts how many attempts have been made at emailing

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year2Quiz2>();
        }
        beenClicked = false;
        SetRandomImage();
        progressText.text = "Question: " + (questionsDoneTwo + 1) + "/" + "1";
    }

    void SetRandomImage()
    {
        int randomImageIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomImageIndex];

        question.text = currentQuestion.question;

        answer1.texture = currentQuestion.image;
        answer2.texture = currentQuestion.image2;
        answer3.texture = currentQuestion.image3;
        answer4.texture = currentQuestion.image4;

        foodnameOne.text = currentQuestion.imageLabel;
        foodnameTwo.text = currentQuestion.image2Label;
        foodnameThree.text = currentQuestion.image3Label;
        foodnameFour.text = currentQuestion.image4Label;

        questionListYear2Part2.Add(currentQuestion.question); // populates the list of questions 
        answerListYear2Part2.Add(currentQuestion.Answer); // populates the list of correct answers

        unansweredQuestions.RemoveAt(randomImageIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDoneTwo == 0)
        {
            questionListYear2Part2.ToArray(); // sets all the lists to arrays for email format
            answerListYear2Part2.ToArray();
            userSelectionListYear2Part2.ToArray();
            SceneManager.LoadScene("year2Results"); // if questions done = all of them, load results screen
        }

        questionsDoneTwo++;
    }

    public void ToggleHelpScreen() // toggles help text
    {
        if (helpCanvas.activeSelf == false)
        {
            helpCanvas.SetActive(true);
        }
        else
        {
            helpCanvas.SetActive(false);
        }
    }

    public void firstAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear2Part2.Add(currentQuestion.imageLabel);
            if (currentQuestion.isCorrect)
            {
                correct.Play(); // plays wrong sound
                answer1.texture = happySmiley.texture;
                score2++;
            }
            else
            {
                answer1.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection
        }
    }

    public void secondAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear2Part2.Add(currentQuestion.image2Label);
            if (currentQuestion.isCorrect2)
            {
                correct.Play(); // plays wrong sound
                answer2.texture = happySmiley.texture;
                score2++;
            }
            else
            {
                answer2.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void thirdAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear2Part2.Add(currentQuestion.image3Label);
            if (currentQuestion.isCorrect3)
            {
                correct.Play(); // plays wrong sound
                answer3.texture = happySmiley.texture;
                score2++;
            }
            else
            {
                answer3.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void fourthAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear2Part2.Add(currentQuestion.image4Label);
            if (currentQuestion.isCorrect4)
            {
                correct.Play(); // plays wrong sound
                answer4.texture = happySmiley.texture;
                score2++;
            }
            else
            {
                answer4.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void exitEarly()
    {
        questionListYear2Part2.ToArray(); // sets all the lists to arrays for email format
        answerListYear2Part2.ToArray();
        userSelectionListYear2Part2.ToArray();
        SceneManager.LoadScene("year2Results"); // if questions done = all of them, load results screen
    }

    public void offlineMode() // allows the user to exit without sending an email
    {
        yearResetScore();
        emailTries = 0;
        SceneManager.LoadScene("yearSelection");
    }

    public void year2SendMail() // Mail send function
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
        mail.Subject = "CC-EAT Year Two: " + currentDate;
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
            "Patient PIN: " + year2GameManager.emailPin + "<br><br>" +
           "The patient's answers are listed below:" + "<br><br>" +
           "<H2> Year 2 Activity 1 </H2>" +
            "<table>" +
            "<tr>" + "<th>" + "Question" + "</th>" + "<th>" + "Correct Answer" + "</th>" + "<th>" + "Child Answer" + "</th>" + "</tr>";
        loopThroughArray();
        mail.Body += "</table>" +
            "<H2> Year 2 Activity 2 </H2>" +
            "<table>" +
            "<tr>" + "<th>" + "Question" + "</th>" + "<th>" + "Correct Answer" + "</th>" + "<th>" + "Child Answer" + "</th>" + "</tr>";
        loopThroughArray2();
        mail.Body += "</table>" +
         "<br>" + "Total score: " + year2Results.score + "/" + year2Results.questionsDone +
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
            SceneManager.LoadScene("year2Results2");
        }
    }

    public void loopThroughArray() // loops through array listing each item instead of reusing code
    {
        for (int i = 0; i < year2GameManager.userSelectionListYear2Part1.Count;)
        {
            mail.Body += "<tr>" + "<td>" + year2GameManager.questionListYear2Part1[i] + "</td>" + "<td>" + year2GameManager.answerListYear2Part1[i] + "</td>" + "<td>" + year2GameManager.userSelectionListYear2Part1[i] + "</td>" + "</tr>";
            i++;
        }
    }

    public void loopThroughArray2() // loops through array listing each item instead of reusing code
    {
        for (int i = 0; i < userSelectionListYear2Part2.Count;)
        {
            mail.Body += "<tr>" + "<td>" + questionListYear2Part2[i] + "</td>" + "<td>" + answerListYear2Part2[i] + "</td>" + "<td>" + userSelectionListYear2Part2[i] + "</td>" + "</tr>";
            i++;
        }
    }

    public void yearResetScore()
    {
        year2GameManager.score = 0;
        year2GameManager.questionsDoneOne = 0;
        score2 = 0;
        questionsDoneTwo = 0;
        year2GameManager.questionListYear2Part1.Clear();
        year2GameManager.userSelectionListYear2Part1.Clear();
        year2GameManager.answerListYear2Part1.Clear();
        questionListYear2Part2.Clear();
        userSelectionListYear2Part2.Clear();
        answerListYear2Part2.Clear();
    }
}
