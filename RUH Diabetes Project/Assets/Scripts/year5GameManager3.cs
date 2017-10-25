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

public class year5GameManager3 : MonoBehaviour
{
    public year5Quiz3[] imagePanel;
    private static List<year5Quiz3> unansweredQuestions;
    private year5Quiz3 currentQuestion;

    public static List<string> questionListYear5Part3 = new List<string>(); // questions list
    public static List<string> answerListYear5Part3 = new List<string>(); // answers list
    public static List<string> userSelectionListYear5Part3 = new List<string>(); // user selections list

    private MailMessage mail = new MailMessage(); // Allows for the email to be constructed in the mail function below
    string currentDate = System.DateTime.Now.ToString("HH:mm:ss d/M/yyyy"); // formats date/time into readable format 

    [SerializeField]
    AudioSource wrong;

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    RawImage questionImage; // The picture of food

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    [SerializeField]
    Text answer1;

    [SerializeField]
    Text answer2;

    [SerializeField]
    Text answer3;

    [SerializeField]
    Text answer4;

    [SerializeField]
    Text answer5;

    [SerializeField]
    RawImage sadSmiley;

    [SerializeField]
    RawImage happySmiley;

    [SerializeField]
    public InputField emailInput; // Where the user types in their email

    [SerializeField]
    private Text emailPlaceholder; // placeholder text in the results scene where user enters email address

    public static int emailTries = 0; // counts how many attempts have been made at emailing

    public static int questionsDoneThree;

    public static int score3;

    private bool beenClicked;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year5Quiz3>();
        }
        SetRandomImage();
    }

    void SetRandomImage()
    {
        int randomImageIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomImageIndex];

        questionImage.texture = currentQuestion.image; // sets the image to the current question image 

        questionListYear5Part3.Add(currentQuestion.question); // populates the list of questions 
        answerListYear5Part3.Add(currentQuestion.Answer); // populates the list of correct answers

        unansweredQuestions.RemoveAt(randomImageIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDoneThree == 4)
        {
            questionListYear5Part3.ToArray(); // sets all the lists to arrays for email format
            answerListYear5Part3.ToArray();
            userSelectionListYear5Part3.ToArray();
            SceneManager.LoadScene("year5Results"); // if questions done = all of them, load results screen
        }

        questionsDoneThree++;
    }


    public void firstAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear5Part3.Add(answer1.text);
            if (currentQuestion.isCorrect)
            {
                correct.Play(); // plays wrong sound
                questionImage.texture = happySmiley.texture;
                score3++;
            }
            else
            {
                wrong.Play();
                questionImage.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection
        }
    }

    public void secondAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear5Part3.Add(answer2.text);
            if (currentQuestion.isCorrect2)
            {
                correct.Play(); // plays wrong sound
                questionImage.texture = happySmiley.texture;
                score3++;
            }
            else
            {
                wrong.Play();
                questionImage.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void thirdAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear5Part3.Add(answer3.text);
            if (currentQuestion.isCorrect3)
            {
                correct.Play(); // plays wrong sound
                questionImage.texture = happySmiley.texture;
                score3++;
            }
            else
            {
                wrong.Play();
                questionImage.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void fourthAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear5Part3.Add(answer4.text);
            if (currentQuestion.isCorrect4)
            {
                correct.Play(); // plays wrong sound
                questionImage.texture = happySmiley.texture;
                score3++;
            }
            else
            {
                wrong.Play();
                questionImage.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void FifthAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear5Part3.Add(answer5.text);
            if (currentQuestion.isCorrect5)
            {
                correct.Play(); // plays wrong sound
                questionImage.texture = happySmiley.texture;
                score3++;
            }
            else
            {
                wrong.Play();
                questionImage.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void year5SendMail() // Mail send function
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
        mail.Subject = "CC-EAT Year Five: " + currentDate;
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
            "Patient PIN: " + year5GameManager.emailPin + "<br><br>" +
           "The patient's answers are listed below:" + "<br><br>" +
           "<H2> Year 5 Activity 1 </H2>" +
            "<table>" +
            "<tr>" + "<th>" + "Question" + "</th>" + "<th>" + "User Answer" + "</th>" + "<th>" + "Correct Answer" + "</th>" + "</tr>";
        loopThroughArray();
        mail.Body += "</table>" +
            "<H2> Year 5 Activity 2 </H2>" +
            "<table>" +
            "<tr>" + "<th>" + "Question" + "</th>" + "<th>" + "User Answer" + "</th>" + "<th>" + "Correct Answer" + "</th>" + "</tr>";
        loopThroughArray2();
        mail.Body += "</table>" +
            "<H2> Year 5 Activity 3 </H2>" +
            "<table>" +
            "<tr>" + "<th>" + "Question" + "</th>" + "<th>" + "User Answer" + "</th>" + "<th>" + "Correct Answer" + "</th>" + "</tr>";
        loopThroughArray3();
        mail.Body += "</table>" +
         "<br>" + "Total score: " + year5Results.score +
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
            SceneManager.LoadScene("year5Results");
        }
    }

    public void loopThroughArray() // loops through array listing each item instead of reusing code
    {
        for (int i = 0; i < 5; i++)
        {
            mail.Body += "<tr>" + "<td>" + year5GameManager.questionListYear5Part1[i] + "</td>" + "<td>" + year5GameManager.userSelectionListYear5Part1[i] + "</td>" + "<td>" + year5GameManager.answerListYear5Part1[i] + "</td>" + "</tr>";
        }
    }

    public void loopThroughArray2() // loops through array listing each item instead of reusing code
    {
        for (int i = 0; i < 2; i++)
        {
            mail.Body += "<tr>" + "<td>" + year5GameManager2.questionListYear5Part2[i] + "</td>" + "<td>" + year5GameManager2.userSelectionListYear5Part2[i] + "</td>" + "<td>" + year5GameManager2.answerListYear5Part2[i] + "</td>" + "</tr>";
        }
    }

    public void loopThroughArray3() // loops through array listing each item instead of reusing code
    {
        for (int i = 0; i < 5; i++)
        {
            mail.Body += "<tr>" + "<td>" + questionListYear5Part3[i] + "</td>" + "<td>" + userSelectionListYear5Part3[i] + "</td>" + "<td>" + answerListYear5Part3[i] + "</td>" + "</tr>";
        }
    }

    public void yearResetScore()
    {
        year5GameManager.score = 0;
        year5GameManager.questionsDone = 0;
        year5GameManager2.score2 = 0;
        year5GameManager2.questionsDoneTwo = 0;
        score3 = 0;
        questionsDoneThree = 0;
        year5GameManager.questionListYear5Part1.Clear();
        year5GameManager.userSelectionListYear5Part1.Clear();
        year5GameManager.answerListYear5Part1.Clear();
        year5GameManager2.questionListYear5Part2.Clear();
        year5GameManager2.userSelectionListYear5Part2.Clear();
        year5GameManager2.answerListYear5Part2.Clear();
        questionListYear5Part3.Clear();
        userSelectionListYear5Part3.Clear();
        answerListYear5Part3.Clear();
    }
}
