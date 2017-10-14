using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class year6Results : MonoBehaviour
{

    [SerializeField]
    public Text resultsText;

    [SerializeField]
    public Text emailInstructions; // tells the user to enter their email

    [SerializeField]
    private Text emailPlaceholder; // email entry placeholder text 

    public static int score;
    private int questionsDone;

    // Use this for initialization - Stores the values 
    void Start()
    {
        emailPlaceholder.text = year1GameManager.emailAddress; // placeholder in the email input becomes the last entered email
        score = year6GameManager.score + year6GameManager2.score2 + year6GameManager3.score3 + +year6GameManager4.score4;
        questionsDone = year6GameManager.questionsDone + year6GameManager2.questionsDoneTwo + +year6GameManager3.questionsDoneThree + year6GameManager4.questionsDoneFour;
        resultsText.text = "You scored " + score.ToString() + "/" + questionsDone;

        if (year6GameManager4.emailTries > 0) // if more than 1 unsuccessful email, the instructions text becomes this 
        {
            emailInstructions.text = "The email did not send. Please try again";
            emailPlaceholder.text = "Please enter your email address here:";
        }
    }

}
