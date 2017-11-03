using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class year6GameManager : MonoBehaviour
{
    public year6Quiz[] imagePanel;
    private static List<year6Quiz> unansweredQuestions;
    private year6Quiz currentQuestion;

    public static List<string> questionListYear6Part1 = new List<string>(); // questions list
    public static List<string> answerListYear6Part1 = new List<string>(); // answers list
    public static List<string> userSelectionListYear6Part1 = new List<string>(); // user selections list

    [SerializeField]
    private GameObject pinEntryCanvas;

    [SerializeField]
    private GameObject year6MenuCanvas;

    [SerializeField]
    public InputField emailPinInput; // Where the user types in their pin

    [SerializeField]
    public Text progressText; 

    [SerializeField]
    public Text resultsText;

    [SerializeField]
    AudioSource wrong;

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    RawImage questionImage; // The picture of food 

    [SerializeField]
    Text foodnameOne;

    [SerializeField]
    RawImage sadSmiley;

    [SerializeField]
    RawImage happySmiley;

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    public static int questionsDone;

    public static int score;

    private bool beenClicked;

    public static string emailPin = ""; // stores the user pin number

    private int radioValue = 0;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year6Quiz>();
        }
        beenClicked = false;
        SetRandomImage();
        progressText.text = "Progress: " + (questionsDone + 1) + "/" + "6";
    }

    public void TogglePinInput() // hides main menu canvas, enables wifi warning 
    {
        if (pinEntryCanvas.activeSelf == true && year6MenuCanvas.activeSelf == false)
        {
            pinEntryCanvas.SetActive(false);
            year6MenuCanvas.SetActive(true);
            emailPin = emailPinInput.text;
        }
        else
        {
            pinEntryCanvas.SetActive(true);
            year6MenuCanvas.SetActive(false);
        }
    }

    void SetRandomImage()
    {
        int randomImageIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomImageIndex];

        questionImage.texture = currentQuestion.image;
        foodnameOne.text = currentQuestion.imageLabel;

        questionListYear6Part1.Add(currentQuestion.question); // populates the list of questions 
        answerListYear6Part1.Add(currentQuestion.carbohydrateAnswer); // populates the list of correct answers

        unansweredQuestions.RemoveAt(randomImageIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDone == 5)
        {
            questionListYear6Part1.ToArray(); // sets all the lists to arrays for email format
            answerListYear6Part1.ToArray();
            userSelectionListYear6Part1.ToArray();
            SceneManager.LoadScene("year6Menu2"); // if questions done = all of them, load results screen
        }

        questionsDone++;
    }

    public void firstRadio()
    {
        radioValue = 1;
    }

    public void secondRadio()
    {
        radioValue = 2;
    }

    public void thirdRadio()
    {
        radioValue = 3;
    }

    public void fourthRadio()
    {
        radioValue = 4;
    }

    public void fifthRadio()
    {
        radioValue = 5;
    }

    public void radioButtonCase()
    {
        switch (radioValue)
        {
            case 1:
                firstAnswer();
                break;
            case 2:
                secondAnswer();
                break;
            case 3:
                thirdAnswer();
                break;
            case 4:
               fourthAnswer();
                break;
            case 5:
               fifthanswer();
                break;
        }
    }

    public void firstAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear6Part1.Add("Glucose");
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
            userSelectionListYear6Part1.Add("Lactose");
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
            userSelectionListYear6Part1.Add("Sugar");
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
            userSelectionListYear6Part1.Add("Fructose");
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

    public void fifthanswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            userSelectionListYear6Part1.Add("Starch");
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

    public void exitEarly()
    {
        questionListYear6Part1.ToArray(); // sets all the lists to arrays for email format
        answerListYear6Part1.ToArray();
        userSelectionListYear6Part1.ToArray();
        SceneManager.LoadScene("year6Results"); // if questions done = all of them, load results screen
    }
}
