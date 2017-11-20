using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class year6Results2 : MonoBehaviour
{

    [SerializeField]
    public Text resultsText;

    [SerializeField]
    public Text emailInstructions; // tells the user to enter their email

    [SerializeField]
    private Text emailPlaceholder; // email entry placeholder text 

    [SerializeField]
    private GameObject results2Canvas; // main canvas 

    [SerializeField]
    private GameObject quitConfirmationCanvas; // asks for confirmation on quit

    public static int score, questionsDone;

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

    public void ToggleResultsScreen() // hides main menu canvas, enables wifi warning 
    {
        if (results2Canvas.activeSelf == true && quitConfirmationCanvas.activeSelf == false)
        {
            results2Canvas.SetActive(false);
            quitConfirmationCanvas.SetActive(true);
        }
        else
        {
            results2Canvas.SetActive(true);
            quitConfirmationCanvas.SetActive(false);
        }
    }
}
