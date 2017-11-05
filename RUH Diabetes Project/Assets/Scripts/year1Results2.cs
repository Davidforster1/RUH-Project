﻿using UnityEngine;
using UnityEngine.UI;

public class year1Results2 : MonoBehaviour
{
    [SerializeField]
    public Text resultsText;

    [SerializeField]
    public Text emailInstructions; // tells the user to enter their email

    [SerializeField]
    private Text emailPlaceholder; // email entry placeholder text 

    private int score;
    private int questionsDone;

    // Use this for initialization - Stores the values 
    void Start()
    {
        emailPlaceholder.text = year1GameManager.emailAddress; // placeholder in the email input becomes the last entered email
        score = year1GameManager.score;
        questionsDone = year1GameManager.questionsDone;
        resultsText.text = "You scored " + score.ToString() + "/" + questionsDone; // shows the score on the results screen

        if (year1GameManager.emailTries > 0) // if more than 1 unsuccessful email, the instructions text becomes this 
        {
            emailInstructions.text = "The email did not send. Please try again";
            emailPlaceholder.text = "Please enter your email address here:";
        }
    }
}