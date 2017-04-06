using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class year6GameManager2 : MonoBehaviour
{
    public year6Quiz2[] questions;
    private static List<year6Quiz2> unansweredQuestions;
    private year6Quiz2 currentQuestion;

    [SerializeField]
    public Text resultsText;

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    AudioSource wrong;

    [SerializeField]
    Text question;

    [SerializeField]
    public Text carbohydrateAnswer;

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    public static int questionsDoneTwo;

    public static int score2;

    private bool beenClicked;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<year6Quiz2>();
        }
        beenClicked = false;
        SetRandomQuestion();
        //Debug.Log(currentQuestion.image + " is " + currentQuestion.isCorrect);
    }
    void SetRandomQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        question.text = currentQuestion.question;

        unansweredQuestions.RemoveAt(randomQuestionIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDoneTwo == 2)
        {
            SceneManager.LoadScene("year6Menu3"); // if questions done = all of them, load results screen
        }

        questionsDoneTwo++;
    }

    public void userSubmitButton()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            // Button click logic here
            if (carbohydrateAnswer.text == currentQuestion.carbohydrateAnswer)
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
