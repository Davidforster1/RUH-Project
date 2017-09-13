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

    [SerializeField]
    public Text emailInstructions; // tells the user to enter their email 

    private int score;
    private int questionsDone;

    // Use this for initialization - Stores the values 
    void Start () {
        score = year1GameManager.score;
        questionsDone = year1GameManager.questionsDone;
        resultsText.text = "You scored " + score.ToString() + "/" + questionsDone;
        if (year1GameManager.emailTries > 0)
        {
            emailInstructions.text = "Please try again";
        }
    }
}
