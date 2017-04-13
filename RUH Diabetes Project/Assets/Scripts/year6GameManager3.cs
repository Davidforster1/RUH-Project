using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class year6GameManager3 : MonoBehaviour
{
    public year6Quiz3[] questions;
    private static List<year6Quiz3> unansweredQuestions;
    private year6Quiz3 currentQuestion;

    [SerializeField]
    public Text resultsText;

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    AudioSource wrong;

    [SerializeField]
    Text question;

    [SerializeField]
    public Text userAnswer;

    [SerializeField]
    public RawImage foodImage;

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    public static int questionsDoneThree;

    public static int score3;

    public static float converted;

    private bool beenClicked;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<year6Quiz3>();
        }
        beenClicked = false;
        SetRandomQuestion();
        //Debug.Log(currentQuestion.image + " is " + currentQuestion.isCorrect);
    }
    void SetRandomQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        question.text = currentQuestion.foodLabel;
        foodImage.texture = currentQuestion.foodImage;

        unansweredQuestions.RemoveAt(randomQuestionIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDoneThree == 7)
        {
            SceneManager.LoadScene("year6Menu4"); // if questions done = all of them, load results screen
        }

        questionsDoneThree++;
    }

    public void userSubmitButton()
    {
        stringtofloat();
        if (!beenClicked)
        {

            beenClicked = true;
            // Button click logic here
            if (converted >= currentQuestion.minAnswer && converted <= currentQuestion.maxAnswer)
            {
                correct.Play();
            }
            else
            {
                wrong.Play(); // plays wrong sound
                score3++;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection
        }
    }

    public void stringtofloat()
    {
        if (userAnswer.text == "")
        {
            return;
        }
        else
        {
            converted = float.Parse(userAnswer.text);
            Debug.Log(userAnswer);
        }
    }
}

