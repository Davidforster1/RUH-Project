using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class year6GameManager : MonoBehaviour
{

    public year6Quiz[] imagePanel;
    private static List<year6Quiz> unansweredQuestions;
    private year6Quiz currentQuestion;

    [SerializeField]
    public Text scoreText;

    [SerializeField]
    public Text resultsText;

    [SerializeField]
    AudioSource wrong;

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    RawImage questionImage; // The picture of food 

    [SerializeField]
    Text foodnameOne;

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    public static int questionsDone;

    public static int score;

    private bool beenClicked;

    private int radioValue = 0;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year6Quiz>();
        }
        beenClicked = false;
        SetRandomImage();
        //Debug.Log(currentQuestion.image + " is " + currentQuestion.isCorrect);
    }
    void SetRandomImage()
    {
        int randomImageIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomImageIndex];

        questionImage.texture = currentQuestion.image;
        foodnameOne.text = currentQuestion.imageLabel;
        
    
        unansweredQuestions.RemoveAt(randomImageIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDone == 5)
        {
            SceneManager.LoadScene("year6Menu2"); // if questions done = all of them, load results screen
        }

        questionsDone++;
    }

    public void firstRadio()
    {
        radioValue = 1;
    }

    public void secondRadio()
    {
        radioValue = 2;
    }

    public void thirdRadio()
    {
        radioValue = 3;
    }

    public void fourthRadio()
    {
        radioValue = 4;
    }

    public void fifthRadio()
    {
        radioValue = 5;
    }

    public void radioButtonCase()
    {
        switch (radioValue)
        {
            case 1:
                firstAnswer();
                break;
            case 2:
                secondAnswer();
                break;
            case 3:
                thirdAnswer();
                break;
            case 4:
               fourthAnswer();
                break;
            case 5:
               fifthanswer();
                break;
        }
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

    public void fourthAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;

            if (currentQuestion.isCorrect4)
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

    public void fifthanswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;

            if (currentQuestion.isCorrect5)
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

}
