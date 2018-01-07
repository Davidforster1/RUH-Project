﻿using UnityEngine;
using UnityEngine.UI;

public class year5Results2 : MonoBehaviour
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

    public static int score;
    public static int questionsDone;
    public playVideo playvideo; // holds the playvideo script object in unity  

    // Use this for initialization - Stores the values 
    void Start()
    {
        emailPlaceholder.text = year1GameManager.emailAddress; // placeholder in the email input becomes the last entered email
        score = year5GameManager.score + year5GameManager2.score2 + year5GameManager3.score3;
        questionsDone = year5GameManager.questionsDone + year5GameManager2.questionsDoneTwo + year5GameManager3.questionsDoneThree;
        resultsText.text = "You scored " + score.ToString() + "/" + questionsDone;

        if (year5GameManager3.emailTries > 0) // if more than 1 unsuccessful email, the instructions text becomes this 
        {
            emailInstructions.text = "The email did not send. Please try again";
            emailPlaceholder.text = "Please enter your email address here:";
            playvideo.enabled = false; // stops playback of the results video if unsuccessful
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
