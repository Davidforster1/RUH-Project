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
}
