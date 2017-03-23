using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class year4GameManager : MonoBehaviour {

    public year4Quiz[] imagePanel;
    private static List<year4Quiz> unansweredQuestions;
    private year4Quiz currentQuestion;

    [SerializeField]
    public Text resultsText;

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    AudioSource wrong;

    [SerializeField]
    public Text foodCarbohydrates;

    [SerializeField]
    RawImage questionImage; // The picture of food 

    [SerializeField]
    RawImage NutritionImage; // The picture of the nutritional table 

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    public static int questionsDone;

    public static int score;

    private bool beenClicked;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year4Quiz>();
        }
        beenClicked = false;
        SetRandomImage();
        //Debug.Log(currentQuestion.image + " is " + currentQuestion.isCorrect);
    }
    void SetRandomImage()
    {
        int randomImageIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomImageIndex];

        NutritionImage.texture = currentQuestion.image2;
        questionImage.texture = currentQuestion.image; // sets the image to the current question image 

        unansweredQuestions.RemoveAt(randomImageIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDone == 11)
        {
            SceneManager.LoadScene("year4Results"); // if questions done = all of them, load results screen
        }

        questionsDone++;
    }

    public void userSubmitButton()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            // Button click logic here
            if (foodCarbohydrates.text == currentQuestion.foodCarbohydrates)
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
