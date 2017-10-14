using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;

public class year5GameManager : MonoBehaviour
{
    public year5Quiz[] imagePanel;
    private static List<year5Quiz> unansweredQuestions;
    private year5Quiz currentQuestion;

    public static List<string> questionListYear5Part1 = new List<string>(); // questions list
    public static List<string> answerListYear5Part1 = new List<string>(); // answers list
    public static List<string> userSelectionListYear5Part1 = new List<string>(); // user selections list

    private MailMessage mail = new MailMessage(); // Allows for the email to be constructed in the mail function below
    string currentDate = System.DateTime.Now.ToString("HH:mm:ss d/M/yyyy"); // formats date/time into readable format 

    [SerializeField]
    AudioSource wrong;

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    Text answer1;

    [SerializeField]
    Text answer2;

    [SerializeField]
    Text answer3;

    [SerializeField]
    Text answer4;

    [SerializeField]
    Text answer5;

    [SerializeField]
    RawImage questionImage; // The picture of food

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    [SerializeField]
    RawImage sadSmiley;

    [SerializeField]
    RawImage happySmiley;

    public static int questionsDone;

    public static int score;

    private bool beenClicked;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year5Quiz>();
        }
        SetRandomImage();
    }

    void SetRandomImage()
    {
        int randomImageIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomImageIndex];

        questionImage.texture = currentQuestion.image; // sets the image to the current question image 
        unansweredQuestions.RemoveAt(randomImageIndex);   // removes a question once it's been answered

        questionListYear5Part1.Add(currentQuestion.question); // populates the list of questions 
        questionListYear5Part1.Add(currentQuestion.Answer); // populates the list of correct answers
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDone == 4)
        {
            questionListYear5Part1.ToArray(); // sets all the lists to arrays for email format
            answerListYear5Part1.ToArray();
            userSelectionListYear5Part1.ToArray();
            SceneManager.LoadScene("year5Activity2"); // if questions done = all of them, load results screen
        }

        questionsDone++;
    }

    public void firstAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear5Part1.Add(answer1.text);
            if (currentQuestion.isCorrect)
            {
                correct.Play(); // plays wrong sound
                questionImage.texture = happySmiley.texture;
                score++;
            }
            else
            {
                wrong.Play();
                questionImage.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection
        }
    }

    public void secondAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear5Part1.Add(answer2.text);
            if (currentQuestion.isCorrect2)
            {
                correct.Play(); // plays wrong sound
                questionImage.texture = happySmiley.texture;
                score++;
            }
            else
            {
                wrong.Play();
                questionImage.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void thirdAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear5Part1.Add(answer3.text);
            if (currentQuestion.isCorrect3)
            {
                correct.Play(); // plays wrong sound
                questionImage.texture = happySmiley.texture;
                score++;
            }
            else
            {
                wrong.Play();
                questionImage.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void fourthAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear5Part1.Add(answer4.text);
            if (currentQuestion.isCorrect4)
            {
                correct.Play(); // plays wrong sound
                questionImage.texture = happySmiley.texture;
                score++;
            }
            else
            {
                wrong.Play();
                questionImage.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void FifthAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear5Part1.Add(answer5.text);
            if (currentQuestion.isCorrect5)
            {
                correct.Play(); // plays wrong sound
                questionImage.texture = happySmiley.texture;
                score++;
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
