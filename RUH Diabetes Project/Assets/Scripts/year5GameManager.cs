using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class year5GameManager : MonoBehaviour
{
    public year5Quiz[] imagePanel;
    private static List<year5Quiz> unansweredQuestions;
    private year5Quiz currentQuestion;

    public static List<string> questionListYear5Part1 = new List<string>(); // questions list
    public static List<string> answerListYear5Part1 = new List<string>(); // answers list
    public static List<string> userSelectionListYear5Part1 = new List<string>(); // user selections list

    [SerializeField]
    public GameObject helpCanvas;

    [SerializeField]
    private GameObject pinEntryCanvas;

    [SerializeField]
    private GameObject year5MenuCanvas;

    [SerializeField]
    public InputField emailPinInput; // Where the user types in their pin

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    Text progressText;

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

    public static string emailPin = ""; // stores the user pin number

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year5Quiz>();
        }
        SetRandomImage();
        progressText.text = "Question: " + (questionsDone + 1) + "/" + "5";
    }

    public void SavePin() // saves the pin 
    {
        emailPin = emailPinInput.text;
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

    void SetRandomImage()
    {
        int randomImageIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomImageIndex];

        questionImage.texture = currentQuestion.image; // sets the image to the current question image 
        unansweredQuestions.RemoveAt(randomImageIndex);   // removes a question once it's been answered

        questionListYear5Part1.Add(currentQuestion.question); // populates the list of questions 
        answerListYear5Part1.Add(currentQuestion.Answer); // populates the list of correct answers
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
                questionImage.texture = sadSmiley.texture;
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }

    public void exitEarly()
    {
        questionListYear5Part1.ToArray(); // sets all the lists to arrays for email format
        answerListYear5Part1.ToArray();
        userSelectionListYear5Part1.ToArray();
        SceneManager.LoadScene("year5Results"); // if questions done = all of them, load results screen
    }
}
