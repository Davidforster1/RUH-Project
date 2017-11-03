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

    public static List<string> questionListYear6Part3 = new List<string>(); // questions list
    public static List<string> answerListYear6Part3 = new List<string>(); // answers list
    public static List<string> userSelectionListYear6Part3 = new List<string>(); // user selections list

    [SerializeField]
    public Text progressText; // current score

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    Text question;

    [SerializeField]
    public Text userAnswer;

    [SerializeField]
    public RawImage foodImage;

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    [SerializeField]
    RawImage sadSmiley;

    [SerializeField]
    RawImage happySmiley;

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
        progressText.text = "Progress: " + (questionsDoneThree + 1) + "/" + "4";
    }
    void SetRandomQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        question.text = currentQuestion.foodLabel;
        foodImage.texture = currentQuestion.foodImage;

        questionListYear6Part3.Add(currentQuestion.foodLabel); // populates the list of questions 
        answerListYear6Part3.Add(currentQuestion.answerRange); // populates the list of correct answers

        unansweredQuestions.RemoveAt(randomQuestionIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDoneThree == 3)
        {
            questionListYear6Part3.ToArray(); // sets all the lists to arrays for email format
            answerListYear6Part3.ToArray();
            userSelectionListYear6Part3.ToArray();
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
            userSelectionListYear6Part3.Add(userAnswer.text);
            if (converted >= currentQuestion.minAnswer && converted <= currentQuestion.maxAnswer)
            {
                foodImage.texture = happySmiley.texture;
                correct.Play();
            }
            else
            {
                foodImage.texture = sadSmiley.texture;
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
        }
    }

    public void exitEarly()
    {
        questionListYear6Part3.ToArray(); // sets all the lists to arrays for email format
        answerListYear6Part3.ToArray();
        userSelectionListYear6Part3.ToArray();
        SceneManager.LoadScene("year6Results"); // if questions done = all of them, load results screen
    }
}

