﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class year5GameManager2 : MonoBehaviour
{
    public year5Quiz2[] questions;
    private static List<year5Quiz2> unansweredQuestions;
    private year5Quiz2 currentQuestion;

    public static List<string> questionListYear5Part2 = new List<string>(); // questions list
    public static List<string> answerListYear5Part2 = new List<string>(); // answers list
    public static List<string> userSelectionListYear5Part2 = new List<string>(); // user selections list

    [SerializeField]
    public GameObject helpCanvas;

    [SerializeField]
    public Text resultsText;

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    Text progressText;

    [SerializeField]
    Text question;

    [SerializeField]
    public Text carbohydrateAnswer;

    [SerializeField]
    GameObject questionImageToggler; // object of the question image for showing if user right/wrong

    [SerializeField]
    GameObject sadSmiley;

    [SerializeField]
    GameObject happySmiley;

    [SerializeField]
    RawImage questionImage;

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    public static int questionsDoneTwo, score2;

    private int questionOrder = 0; // keeps track of question number, instead of it being random 

    private bool beenClicked;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<year5Quiz2>();
        }
        beenClicked = false;
        SetRandomQuestion();
        progressText.text = "Question: " + (questionsDoneTwo + 1) + "/" + "2";
    }
    void SetRandomQuestion()
    {
        currentQuestion = unansweredQuestions[questionOrder];

        question.text = currentQuestion.question;

        questionListYear5Part2.Add(currentQuestion.question); // populates the list of questions 
        answerListYear5Part2.Add(currentQuestion.carbohydrateAnswer); // populates the list of correct answers

        unansweredQuestions.RemoveAt(questionOrder);   // removes a question once it's been answered
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

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDoneTwo == 1)
        {
            questionListYear5Part2.ToArray(); // sets all the lists to arrays for email format
            answerListYear5Part2.ToArray();
            userSelectionListYear5Part2.ToArray();
            SceneManager.LoadScene("year5Menu3"); // if questions done = all of them, load results screen
        }

        questionOrder++;  questionsDoneTwo++; 
    }

    public void userSubmitButton()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear5Part2.Add(carbohydrateAnswer.text + "g");
            if (carbohydrateAnswer.text == currentQuestion.carbohydrateAnswer)
            {
                correct.Play(); // plays wrong sound
                happySmiley.SetActive(true); sadSmiley.SetActive(false);
                questionImageToggler.SetActive(false);
                score2++;
            }
            else
            {
                sadSmiley.SetActive(true); happySmiley.SetActive(false);
                questionImageToggler.SetActive(false);
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection
        }
    }

    public void exitEarly()
    {
        questionListYear5Part2.ToArray(); // sets all the lists to arrays for email format
        answerListYear5Part2.ToArray();
        userSelectionListYear5Part2.ToArray();
        SceneManager.LoadScene("year5Results"); // if questions done = all of them, load results screen
    }
}
