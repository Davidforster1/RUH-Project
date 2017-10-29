﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class year2GameManager : MonoBehaviour
{

    public year2Quiz[] imagePanel;
    private static List<year2Quiz> unansweredQuestions;
    private year2Quiz currentQuestion;

    public static List<string> questionListYear2Part1 = new List<string>(); // questions list
    public static List<string> answerListYear2Part1 = new List<string>(); // answers list
    public static List<string> userSelectionListYear2Part1 = new List<string>(); // user selections list

    [SerializeField]
    private GameObject pinEntryCanvas;

    [SerializeField]
    private GameObject year2MenuCanvas;

    [SerializeField]
    public InputField emailPinInput; // Where the user types in their pin

    [SerializeField]
    public Text scoreText;

    [SerializeField]
    public Text resultsText;

    [SerializeField]
    AudioSource wrong;

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    RawImage sadSmiley;

    [SerializeField]
    RawImage happySmiley;

    [SerializeField]
    RawImage answer1; // The picture of food 

    [SerializeField]
    RawImage answer2; // The picture of food 

    [SerializeField]
    RawImage answer3; // The picture of food 

    [SerializeField]
    Text foodnameOne;

    [SerializeField]
    Text foodnameTwo;

    [SerializeField]
    Text foodnameThree;

    [SerializeField]
    Text question;

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    public static int questionsDoneOne;

    public static int score;

    private bool beenClicked;

    public static string emailPin = ""; // stores the user pin number

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year2Quiz>();
        }
        beenClicked = false;
        SetRandomImage(); // sets the question
    }

    public void TogglePinInput() // hides main menu canvas, enables wifi warning 
    {
        pinEntryCanvas.SetActive(false);
        year2MenuCanvas.SetActive(true);
        emailPin = emailPinInput.text;
    }

    void SetRandomImage()
    {
        int randomImageIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomImageIndex];

        question.text = currentQuestion.question;

        answer1.texture = currentQuestion.image;
        answer2.texture = currentQuestion.image2;
        answer3.texture = currentQuestion.image3;

        foodnameOne.text = currentQuestion.imageLabel;
        foodnameTwo.text = currentQuestion.image2Label;
        foodnameThree.text = currentQuestion.image3Label;

        questionListYear2Part1.Add(currentQuestion.question); // populates the list of questions 
        answerListYear2Part1.Add(currentQuestion.answer); // populates the list of correct answers

        unansweredQuestions.RemoveAt(randomImageIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDoneOne == 7)
        {
            questionListYear2Part1.ToArray(); // sets all the lists to arrays for email format
            answerListYear2Part1.ToArray();
            userSelectionListYear2Part1.ToArray();
            SceneManager.LoadScene("year2Menu2"); // if questions done = all of them, load results screen
        }

        questionsDoneOne++;
    }

    public void firstAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear2Part1.Add(currentQuestion.imageLabel);
            if (currentQuestion.isCorrect)
            {
                correct.Play(); // plays wrong sound
                answer1.texture = happySmiley.texture;
                score++;
            }
            else
            {
                wrong.Play();
                answer1.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection
        }
    }

    public void secondAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear2Part1.Add(currentQuestion.image2Label);
            if (currentQuestion.isCorrect2)
            {
                correct.Play(); // plays wrong sound
                answer2.texture = happySmiley.texture;
                score++;
            }
            else
            {
                wrong.Play();
                answer2.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void thirdAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear2Part1.Add(currentQuestion.image3Label);
            if (currentQuestion.isCorrect3)
            {
                correct.Play(); // plays wrong sound
                answer3.texture = happySmiley.texture;
                score++;
            }
            else
            {
                wrong.Play();
                answer3.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void exitEarly()
    {
        questionListYear2Part1.ToArray(); // sets all the lists to arrays for email format
        answerListYear2Part1.ToArray();
        userSelectionListYear2Part1.ToArray();
        SceneManager.LoadScene("year2Results"); // if questions done = all of them, load results screen
    }
}
