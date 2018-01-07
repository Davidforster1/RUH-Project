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

public class year4GameManager : MonoBehaviour {

    public year4Quiz[] imagePanel;
    private static List<year4Quiz> unansweredQuestions;
    private year4Quiz currentQuestion;

    public static List<string> questionList = new List<string>(); // questions list
    public static List<string> answerList = new List<string>(); // answers list
    public static List<string> userSelectionList = new List<string>(); // user selections list

    private MailMessage mail = new MailMessage(); // Allows for the email to be constructed in the mail function below
    string currentDate = System.DateTime.Now.ToString("HH:mm:ss d/M/yyyy"); // formats date/time into readable format 

    [SerializeField]
    public GameObject helpCanvas;

    [SerializeField]
    private GameObject pinEntryCanvas;

    [SerializeField]
    private GameObject year4MenuCanvas;

    [SerializeField]
    public InputField emailInput; // Where the user types in their email

    [SerializeField]
    public InputField emailPinInput; // Where the user types in their pin

    [SerializeField]
    public Text progressText;

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    public Text foodCarbohydrates;

    [SerializeField]
    RawImage questionImage; // The picture of food 

    [SerializeField]
    RawImage NutritionImage; // The picture of the nutritional table 

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

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year4Quiz>();
        }
        beenClicked = false;
        SetRandomImage();
        progressText.text = "Question: " + (questionsDone + 1) + "/" + "10";
    }

    public void SavePin() // saves the pin 
    {
        emailPin = emailPinInput.text;
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

    public void offlineMode() // allows the user to exit without sending an email
    {
        yearResetScore();
        emailTries = 0;
        SceneManager.LoadScene("yearSelection");
    }

    void SetRandomImage()
    {
        int randomImageIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomImageIndex];

        NutritionImage.texture = currentQuestion.image2;
        questionImage.texture = currentQuestion.image; // sets the image to the current question image 

        questionList.Add(currentQuestion.question);
        answerList.Add(currentQuestion.foodCarbohydrates);

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
            SceneManager.LoadScene("year4Results"); // if questions done = all of them, load results screen
        }

        questionsDone++;
    }

    public void userSubmitButton()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionList.Add(foodCarbohydrates.text);
            if (foodCarbohydrates.text == currentQuestion.foodCarbohydrates)
            {
                correct.Play(); // plays wrong sound
                NutritionImage.texture = happySmiley.texture;
                score++;
            }
              else
            {
                NutritionImage.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection
        }
    }

    public void exitEarly()
    {
        questionList.ToArray(); // sets all the lists to arrays for email format
        answerList.ToArray();
        userSelectionList.ToArray();
        SceneManager.LoadScene("year4Results"); // if questions done = all of them, load results screen
    }

    public void year4SendMail() // Mail send function
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
        mail.Subject = "CC-EAT Year Four: " + currentDate;
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
           "<H2> Year 4 Activity 1 </H2>" +
            "<table>" +
            "<tr>" + "<th>" + "Question" + "</th>" + "<th>" + "Correct Answer" + "</th>" + "<th>" + "Child Answer" + "</th>" + "</tr>";
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
            SceneManager.LoadScene("year4Results2");
        }
    }

    public void loopThroughArray() // loops through array listing each item instead of reusing code
    {
        for (int i = 0; i < userSelectionList.Count;)
        {
            mail.Body += "<tr>" + "<td>" + questionList[i] + "</td>" + "<td>" + answerList[i] + "g" + "</td>" + "<td>" + userSelectionList[i] + "g" + "</td>" + "</tr>";
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
