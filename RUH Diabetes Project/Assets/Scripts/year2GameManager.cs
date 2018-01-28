using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class year2GameManager : MonoBehaviour
{

    public year2Quiz[] imagePanel;
    private static List<year2Quiz> unansweredQuestions;
    private year2Quiz currentQuestion;

    public static List<string> questionListYear2Part1 = new List<string>(); // questions list
    public static List<string> answerListYear2Part1 = new List<string>(); // answers list
    public static List<string> userSelectionListYear2Part1 = new List<string>(); // user selections list
    public static List<string> tempemailcreator = new List<string>(); // holds user selections then adds to the user selection list as 1 entry

    [SerializeField]
    public GameObject helpCanvas;

    [SerializeField]
    private GameObject pinEntryCanvas;

    [SerializeField]
    private GameObject year2MenuCanvas;

    [SerializeField]
    public InputField emailPinInput; // Where the user types in their pin

    [SerializeField]
    public Text progressText; 

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    GameObject questionImageToggler; // object of the question image for showing if user right/wrong

    [SerializeField]
    GameObject sadSmiley;

    [SerializeField]
    GameObject happySmiley;

    [SerializeField]
    RawImage answer1; // The picture of food 

    [SerializeField]
    RawImage answer2; // The picture of food 

    [SerializeField]
    RawImage answer3; // The picture of food 

    [SerializeField]
    Text foodnameOne;

    [SerializeField]
    Text foodnameTwo;

    [SerializeField]
    Text foodnameThree;

    [SerializeField]
    Text question;

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    public static int questionsDoneOne;

    public static int score;

    private bool beenClicked;

    public static string emailPin = ""; // stores the user pin number

    public bool touched1, touched2, touched3 = false; // stores which ones are currently selected

    public static int pointsAvailable;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year2Quiz>();
        }
        beenClicked = false;
        SetRandomImage(); // sets the question
        progressText.text = "Question: " + (questionsDoneOne + 1) + "/" + "9";
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

        question.text = currentQuestion.question;

        answer1.texture = currentQuestion.image;
        answer2.texture = currentQuestion.image2;
        answer3.texture = currentQuestion.image3;

        foodnameOne.text = currentQuestion.imageLabel;
        foodnameTwo.text = currentQuestion.image2Label;
        foodnameThree.text = currentQuestion.image3Label;

        questionListYear2Part1.Add(currentQuestion.question); // populates the list of questions 
        answerListYear2Part1.Add(currentQuestion.answer); // populates the list of correct answers

        unansweredQuestions.RemoveAt(randomImageIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDoneOne == 8)
        {
            questionListYear2Part1.ToArray(); // sets all the lists to arrays for email format
            answerListYear2Part1.ToArray();
            userSelectionListYear2Part1.ToArray();
            SceneManager.LoadScene("year2Menu2"); // if questions done = all of them, load results screen
        }

        questionsDoneOne++;
    }

    public void Touchedlogic1()
{
        if (touched1 == false)
        {
            touched1 = true;
            Color temp = answer1.color; // holds colour value
            temp.a = 0.5f; // alpha = 50%
            answer1.color = temp; // applies temp colour to image
        }
        // light up image or something 
        else
        {
            touched1 = false;
            Color temp = answer1.color; // holds colour value
            temp.a = 1f; // alpha = 1
            answer1.color = temp; // applies temp colour to image
        }
}

    public void Touchedlogic2()
    {
        if (touched2 == false)
        {
            touched2 = true;
            Color temp = answer2.color; // holds colour value
            temp.a = 0.5f; // alpha = 50%
            answer2.color = temp; // applies temp colour to image
        }
        // light up image or something 
        else
        {
            touched2 = false;
            Color temp = answer2.color; // holds colour value
            temp.a = 1f; // alpha = 1
            answer2.color = temp; // applies temp colour to image
        }
    }

    public void Touchedlogic3()
    {
        if (touched3 == false)
        {
            touched3 = true;
            Color temp = answer3.color; // holds colour value
            temp.a = 0.5f; // alpha = 50%
            answer3.color = temp; // applies temp colour to image
        }
        else
        {
            touched3 = false;
            Color temp = answer3.color; // holds colour value
            temp.a = 1f; // alpha = 1
            answer3.color = temp; // applies temp colour to image
        }
    }

    public void answerselections()
    {
        {
            if (touched1 == true)
            {
                firstAnswer();
                tempemailcreator.Add(currentQuestion.imageLabel);
                if (currentQuestion.isCorrect)
                {
                    pointsAvailable++;
                }
            }
            else
            {
                tempemailcreator.Add(" ");
                if (currentQuestion.isCorrect)
                {
                    pointsAvailable++;
                }
            }
            if (touched2 == true)
            {
                secondAnswer();
                tempemailcreator.Add(currentQuestion.image2Label);
                if (currentQuestion.isCorrect2)
                {
                    pointsAvailable++;
                }
            }
            else
            {
                tempemailcreator.Add(" ");
                if (currentQuestion.isCorrect2)
                {
                    pointsAvailable++;
                }
            }
            if (touched3 == true)
            {
                thirdAnswer();
                tempemailcreator.Add(currentQuestion.image3Label);
                if (currentQuestion.isCorrect3)
                {
                    pointsAvailable++;
                }
            }
            else
            {
                tempemailcreator.Add(" ");
                if (currentQuestion.isCorrect3)
                {
                    pointsAvailable++;
                }
            }

            if (currentQuestion.isCorrect && touched1 == true || currentQuestion.isCorrect2 && touched2 == true || currentQuestion.isCorrect3 && touched3 == true)
            {
                correct.Play(); // plays wrong sound
                happySmiley.SetActive(true); sadSmiley.SetActive(false);
                questionImageToggler.SetActive(false);
                score++;
            }
            else
            {
                sadSmiley.SetActive(true); happySmiley.SetActive(false);
                questionImageToggler.SetActive(false);
            }

            tempemailcreator.ToArray();
            userSelectionListYear2Part1.Add(tempemailcreator[0] + tempemailcreator[1] +  tempemailcreator[2]); // combines 3 possible answers into one list entry
            tempemailcreator.Clear(); // resets list for next question
            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection
        }
    }

    public void firstAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            if (currentQuestion.isCorrect)
            {
                score++;
            }
        }
    }

    public void secondAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            if (currentQuestion.isCorrect2)
            {
                score++;
            }
        }
    }

    public void thirdAnswer()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            if (currentQuestion.isCorrect3)
            {
                score++;
            }
        }
    }

    public void exitEarly()
    {
        questionListYear2Part1.ToArray(); // sets all the lists to arrays for email format
        answerListYear2Part1.ToArray();
        userSelectionListYear2Part1.ToArray();
        SceneManager.LoadScene("year2Results"); // if questions done = all of them, load results screen
    }
}
