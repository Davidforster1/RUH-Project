﻿using UnityEngine;
using UnityEngine.UI;

public class year5Results : MonoBehaviour
{
    [SerializeField]
    public Text resultsText;

    [SerializeField]
    public Text emailInstructions; // tells the user to enter their email

    [SerializeField]
    private Text emailPlaceholder; // email entry placeholder text 

    public static int score;
    public static int questionsDone;


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
        }
    }
}
