using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;


public class year1Results : MonoBehaviour {

    [SerializeField]
    public Text resultsText;

    [SerializeField]
    public Text emailInstructions; // tells the user to enter their email
    
    [SerializeField]
    private Text emailPlaceholder;

    private int score;
    private int questionsDone;

    // Use this for initialization - Stores the values 
    void Start ()
    {
        emailPlaceholder.text = year1GameManager.emailAddress;
        score = year1GameManager.score;
        questionsDone = year1GameManager.questionsDone;
        resultsText.text = "You scored " + score.ToString() + "/" + questionsDone;

        if (year1GameManager.emailTries > 0)
        {
            emailInstructions.text = "The email did not send. Please try again";
            emailPlaceholder.text = "Please enter your email address here:";
        }
    }
}
