using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class year1GameManager : MonoBehaviour {

    public year1Quiz[] imagePanel;
    private static List<year1Quiz> unansweredQuestions;
    private year1Quiz currentQuestion;

    [SerializeField]
    public Text scoreText;

    [SerializeField]
    public Text resultsText;

    [SerializeField]
    public Text foodName;

    [SerializeField]
    AudioSource wrong;

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    RawImage questionImage; // The picture of food 

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    public static int questionsDone;

    public static int score;

    private bool beenClicked;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year1Quiz>();
        }
        beenClicked = false;
        SetRandomImage();
        //Debug.Log(currentQuestion.image + " is " + currentQuestion.isCorrect);
    }   
    void SetRandomImage()
    {
        int randomImageIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomImageIndex];

        questionImage.texture = currentQuestion.image; // sets the image to the current question image 
        foodName.text = currentQuestion.foodName;

        unansweredQuestions.RemoveAt(randomImageIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDone == 9)
        {
            SceneManager.LoadScene("year1Results"); // if questions done = all of them, load results screen
        }

        questionsDone++;
    }

    public void userSelectTrue()
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

    public void userSelectFalse()
    {
        if (!beenClicked)
        {
            beenClicked = true;

            if (!currentQuestion.isCorrect)
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
