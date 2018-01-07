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

    public static List<string> questionListYear6Part2 = new List<string>(); // questions list
    public static List<string> answerListYear6Part2 = new List<string>(); // answers list
    public static List<string> userSelectionListYear6Part2 = new List<string>(); // user selections list

    [SerializeField]
    public GameObject helpCanvas;

    [SerializeField]
    public Text progressText; 

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    Text question;

    [SerializeField]
    public Text carbohydrateAnswer;

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    [SerializeField]
    RawImage sadSmiley;

    [SerializeField]
    RawImage happySmiley;

    [SerializeField]
    RawImage questionImage;

    public static int questionsDoneTwo, score2;

    private int questionOrder = 0; // keeps track of question number, instead of it being random 

    private bool beenClicked;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<year6Quiz2>();
        }
        beenClicked = false;
        SetQuestion();
        progressText.text = "Question: " + (questionsDoneTwo + 1) + "/" + "3";
    }
    void SetQuestion()
    {
        currentQuestion = unansweredQuestions[questionOrder];

        question.text = currentQuestion.question;

        questionListYear6Part2.Add(currentQuestion.question); // populates the list of questions 
        answerListYear6Part2.Add(currentQuestion.carbohydrateAnswer); // populates the list of correct answers
        unansweredQuestions.RemoveAt(questionOrder);   // removes a question once it's been answered
    }

    public void ToggleHelpScreen() // toggles help text
    {
        if (helpCanvas.activeSelf == false)
        {
            helpCanvas.SetActive(true);
        }
        else
        {
            helpCanvas.SetActive(false);
        }
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDoneTwo == 2)
        {
            questionListYear6Part2.ToArray(); // sets all the lists to arrays for email format
            answerListYear6Part2.ToArray();
            userSelectionListYear6Part2.ToArray();
            SceneManager.LoadScene("year6Menu3"); // if questions done = all of them, load results screen
        }
        questionOrder++; questionsDoneTwo++;
    }

    public void userSubmitButton()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear6Part2.Add(carbohydrateAnswer.text);
            if (carbohydrateAnswer.text == currentQuestion.carbohydrateAnswer)
            {
                correct.Play(); // plays wrong sound
                questionImage.texture = happySmiley.texture;
                score2++;
            }
            else
            {
                questionImage.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection
        }
    }

    public void exitEarly()
    {
        questionListYear6Part2.ToArray(); // sets all the lists to arrays for email format
        answerListYear6Part2.ToArray();
        userSelectionListYear6Part2.ToArray();
        SceneManager.LoadScene("year6Results"); // if questions done = all of them, load results screen
    }
}
