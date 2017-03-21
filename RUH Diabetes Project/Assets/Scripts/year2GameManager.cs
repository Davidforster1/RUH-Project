using UnityEngine;
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
    private float timeBetweenQuestions = 2f; // delay between questions 

    public static int questionsDone;

    public static int score;

    private bool beenClicked;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year2Quiz>();
        }
        beenClicked = false;
        SetRandomImage();
        //Debug.Log(currentQuestion.image + " is " + currentQuestion.isCorrect);
    }
    void SetRandomImage()
    {
        int randomImageIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomImageIndex];

        answer1.texture = currentQuestion.image;
        answer2.texture = currentQuestion.image2;
        answer3.texture = currentQuestion.image3;

        unansweredQuestions.RemoveAt(randomImageIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDone == 1)
        {
            SceneManager.LoadScene("year2Results"); // if questions done = all of them, load results screen
        }

        questionsDone++;
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
                score++;
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
                score++;
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
                score++;
            }
            else
            {
                wrong.Play();
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void year1ResetScore()
    {
        score = 0;
        questionsDone = 0;
    }
}
