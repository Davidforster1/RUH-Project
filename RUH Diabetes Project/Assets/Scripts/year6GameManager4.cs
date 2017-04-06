using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class year6GameManager4 : MonoBehaviour
{
    public year6Quiz4[] questions;
    private static List<year6Quiz4> unansweredQuestions;
    private year6Quiz4 currentQuestion;

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

    public static int questionsDoneFour;

    public static int score4;

    private bool beenClicked;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<year6Quiz4>();
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

        if (questionsDoneFour == 3)
        {
            SceneManager.LoadScene("year6Results"); // if questions done = all of them, load results screen
        }

        questionsDoneFour++;
    }

       public void userSubmitButton()
       {
           if (!beenClicked)
           {
               beenClicked = true;
               // Button click logic here
               if (userAnswer.text == currentQuestion.userAnswer)
               {
                   correct.Play(); // plays wrong sound
                   score4++;
               }
               else
               {
                   wrong.Play();
               }

               StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection
           }
       }
}
