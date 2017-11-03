using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class year3GameManager : MonoBehaviour {


    public year3Quiz[] imagePanel;
    private static List<year3Quiz> unansweredQuestions;
    private year3Quiz currentQuestion;

    public static List<string> questionList = new List<string>(); // questions list
    public static List<string> answerList = new List<string>(); // answers list
    public static List<string> userSelectionList = new List<string>(); // user selections list

    private MailMessage mail = new MailMessage(); // Allows for the email to be constructed in the mail function below
    string currentDate = System.DateTime.Now.ToString("HH:mm:ss d/M/yyyy"); // formats date/time into readable format 

    [SerializeField]
    private GameObject pinEntryCanvas;

    [SerializeField]
    private GameObject year3MenuCanvas;

    [SerializeField]
    public InputField emailInput; // Where the user types in their email

    [SerializeField]
    public InputField emailPinInput; // Where the user types in their pin

    [SerializeField]
    public Text progressText;

    [SerializeField]
    AudioSource wrong;

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    RawImage answer1; // The picture of food 

    [SerializeField]
    RawImage answer2; // The picture of food 

    [SerializeField] // food label
    Text foodnameOne;

    [SerializeField] // food label 
    Text foodnameTwo;

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    [SerializeField]
    private Text emailPlaceholder; // placeholder text in the results scene where user enters email address

    [SerializeField]
    RawImage sadSmiley; // happy smiley face on answer 

    [SerializeField]
    RawImage happySmiley; // sad smiley face on answer 

    public static int questionsDone;

    public static int score;

    private bool beenClicked;

    public static int emailTries = 0; // counts how many attempts have been made at emailing

    public static string emailPin = "";

    // Use this for initialization
    void Start () {

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year3Quiz>();
        }
        beenClicked = false;
        SetRandomImage();
        progressText.text = "Progress: " + (questionsDone + 1) + "/" + "9";
    }

    public void TogglePinInput() // hides main menu canvas, enables wifi warning 
    {
        if (pinEntryCanvas.activeSelf == true && year3MenuCanvas.activeSelf == false)
        {
            pinEntryCanvas.SetActive(false);
            year3MenuCanvas.SetActive(true);
            emailPin = emailPinInput.text;
        }
        else
        {
            pinEntryCanvas.SetActive(true);
            year3MenuCanvas.SetActive(false);
        }
    }

    void SetRandomImage()
    {
        int randomImageIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomImageIndex];

        answer1.texture = currentQuestion.image;
        answer2.texture = currentQuestion.image2;

        foodnameOne.text = currentQuestion.imageLabel;
        foodnameTwo.text = currentQuestion.image2Label;

        questionList.Add(currentQuestion.question);
        answerList.Add(currentQuestion.answer);

        unansweredQuestions.RemoveAt(randomImageIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDone == 8)
        {
            questionList.ToArray(); // sets all the lists to arrays for email format
            answerList.ToArray();
            userSelectionList.ToArray();
            SceneManager.LoadScene("year3Results"); // if questions done = all of them, load results screen
        }

        questionsDone++;
    }

    public void firstAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionList.Add(currentQuestion.imageLabel);
            if (currentQuestion.isCorrect)
            {
                correct.Play(); // plays wrong sound
                answer1.texture = happySmiley.texture;
                score++;
            }
            else
            {
                wrong.Play();
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
            userSelectionList.Add(currentQuestion.image2Label);
            if (currentQuestion.isCorrect2)
            {
                correct.Play(); // plays wrong sound
                answer2.texture = happySmiley.texture;
                score++;
            }
            else
            {
                wrong.Play();
                answer2.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection
        }
    }

    public void exitEarly()
    {
        questionList.ToArray(); // sets all the lists to arrays for email format
        answerList.ToArray();
        userSelectionList.ToArray();
        SceneManager.LoadScene("year3Results"); // if questions done = all of them, load results screen
    }

    public void year3SendMail() // Mail send function
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
        mail.Subject = "CC-EAT Year Three: " + currentDate;
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
            "Patient PIN: " + emailPin + "<br><br>" +
           "The patient's answers are listed below:" + "<br><br>" +
           "<H2> Year 3 Activity 1 </H2>" +
            "<table>" +
            "<tr>" + "<th>" + "Question" + "</th>" + "<th>" + "User Answer" + "</th>" + "<th>" + "Correct Answer" + "</th>" + "</tr>";
        loopThroughArray();
        mail.Body += "</table>" +
         "<br>" + "Total score: " + score + "/" + questionsDone +
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
            SceneManager.LoadScene("year3Results");
        }
    }

    public void loopThroughArray() // loops through array listing each item instead of reusing code
    {
        for (int i = 0; i < userSelectionList.Count;)
        {
            mail.Body += "<tr>" + "<td>" + questionList[i] + "</td>" + "<td>" + userSelectionList[i] + "</td>" + "<td>" + answerList[i] + "</td>" + "</tr>";
            i++;
        }
    }

    public void yearResetScore() // score resets
    {
        score = 0;
        questionsDone = 0;
        questionList.Clear();
        userSelectionList.Clear();
        answerList.Clear();
    }

}
