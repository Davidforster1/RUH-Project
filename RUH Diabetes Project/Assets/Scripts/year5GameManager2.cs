using UnityEngine;
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
    RawImage sadSmiley;

    [SerializeField]
    RawImage happySmiley;

    [SerializeField]
    RawImage questionImage;

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    public static int questionsDoneTwo;

    public static int score2;

    private bool beenClicked;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<year5Quiz2>();
        }
        beenClicked = false;
        SetRandomQuestion();
    }
    void SetRandomQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        question.text = currentQuestion.question;

        questionListYear5Part2.Add(currentQuestion.question); // populates the list of questions 
        answerListYear5Part2.Add(currentQuestion.carbohydrateAnswer); // populates the list of correct answers

        unansweredQuestions.RemoveAt(randomQuestionIndex);   // removes a question once it's been answered
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

        questionsDoneTwo++;
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
                questionImage.texture = happySmiley.texture;
                score2++;
            }
            else
            {
                wrong.Play();
                questionImage.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection
        }
    }
}
