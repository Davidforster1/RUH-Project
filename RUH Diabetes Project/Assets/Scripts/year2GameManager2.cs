using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class year2GameManager2 : MonoBehaviour
{

    public year2Quiz2[] imagePanel;
    private static List<year2Quiz2> unansweredQuestions;
    private year2Quiz2 currentQuestion;

    [SerializeField]
    public Text scoreText;

    [SerializeField]
    public Text resultsText;

    [SerializeField]
    AudioSource wrong;

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    RawImage answer1; // The picture of food 

    [SerializeField]
    RawImage answer2; // The picture of food 

    [SerializeField]
    RawImage answer3; // The picture of food 

    [SerializeField]
    RawImage answer4; // The picture of food 

    [SerializeField]
    Text foodnameOne;

    [SerializeField]
    Text foodnameTwo;

    [SerializeField]
    Text foodnameThree;

    [SerializeField]
    Text foodnameFour;

    [SerializeField]
    Text question;

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    public static int questionsDoneTwo;

    // public static int totalQuestionsDone;

    public static int score2;

    private bool beenClicked;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year2Quiz2>();
        }
        beenClicked = false;
        SetRandomImage();
        //Debug.Log(currentQuestion.image + " is " + currentQuestion.isCorrect);
    }
    void SetRandomImage()
    {
        int randomImageIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomImageIndex];

        question.text = currentQuestion.question;

        answer1.texture = currentQuestion.image;
        answer2.texture = currentQuestion.image2;
        answer3.texture = currentQuestion.image3;
        answer4.texture = currentQuestion.image4;

        foodnameOne.text = currentQuestion.imageLabel;
        foodnameTwo.text = currentQuestion.image2Label;
        foodnameThree.text = currentQuestion.image3Label;
        foodnameFour.text = currentQuestion.image4Label;

        unansweredQuestions.RemoveAt(randomImageIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDoneTwo == 1)
        {
            SceneManager.LoadScene("year2Results"); // if questions done = all of them, load results screen
        }

        questionsDoneTwo++;
    }

    public void firstAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            // Button click logic here
            if (currentQuestion.isCorrect)
            {
                correct.Play(); // plays wrong sound
                score2++;
            }
            else
            {
                wrong.Play();
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection
        }
    }

    public void secondAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;

            if (currentQuestion.isCorrect2)
            {
                correct.Play(); // plays wrong sound
                score2++;
            }
            else
            {
                wrong.Play();
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void thirdAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;

            if (currentQuestion.isCorrect3)
            {
                correct.Play(); // plays wrong sound
                score2++;
            }
            else
            {
                wrong.Play();
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void fourthAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;

            if (currentQuestion.isCorrect4)
            {
                correct.Play(); // plays wrong sound
                score2++;
            }
            else
            {
                wrong.Play();
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

}
