using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class year5GameManager : MonoBehaviour
{
    public year5Quiz[] imagePanel;
    private static List<year5Quiz> unansweredQuestions;
    private year5Quiz currentQuestion;

    [SerializeField]
    AudioSource wrong;

    [SerializeField]
    AudioSource correct;

    [SerializeField]
    RawImage questionImage; // The picture of food

    [SerializeField]
    private float timeBetweenQuestions = 2f; // delay between questions 

    public static int questionsDone;

    public static int score;

    private bool lineConnected;

    private int lineNumber = 0;

    //reference to LineRenderer component
    private LineRenderer line;
    //car to store touch position on the screen
    private Vector3 touchPos;
    //assign a material to the Line Renderer in the Inspector
    public Material material;
    //number of lines drawn
    private int currLines = 0;

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year5Quiz>();
        }
        lineConnected = false;
        SetRandomImage();
        //Debug.Log(currentQuestion.image + " is " + currentQuestion.isCorrect);
    }

    void SetRandomImage()
    {
        int randomImageIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomImageIndex];

        questionImage.texture = currentQuestion.image; // sets the image to the current question image 
        unansweredQuestions.RemoveAt(randomImageIndex);   // removes a question once it's been answered
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the scene to update question

        if (questionsDone == 4)
        {
            SceneManager.LoadScene("year5Results"); // if questions done = all of them, load results screen
        }

        questionsDone++;
    }


    void Update()
    {
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);
        //Create new Line on touch 

        if (touch.phase == TouchPhase.Ended && line)
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count > 0)
            {
                foreach (var go in raycastResults)
                {
                    // checks to see if line collides with the correct answer
                    if (currentQuestion.value == go.gameObject.name)
                    {
                        userSelectTrue();
                    }
                    else
                    {
                        userSelectFalse();
                    }

                }
            }
        }

        if (touch.phase == TouchPhase.Began)
        {              
            //check if there is no line renderer created
            if (line == null)
            {
                //create the line
                createLine();
            }

            //get the touch position
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //set the z co ordinate to 0 as we are only interested in the xy axes
            touchPos.z = 0;
            //set the start point and end point of the line renderer
            line.SetPosition(0, touchPos);
            line.SetPosition(1, touchPos);

        }
        //if line renderer exists and touch stops
        else if (touch.phase == TouchPhase.Ended && line)
        {
            
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPos.z = 0;
            //set the end point of the line renderer to current touch position
            line.SetPosition(1, touchPos);
            //set line as null once the line is created
            line = null;
            currLines++;
           
        }
        //if touch is moved, line creates
        else if (touch.phase == TouchPhase.Moved && line)
        {

            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPos.z = 0;
            //set the end position as current position but dont set line as null as the finger is not lifted
            line.SetPosition(1, touchPos);

        }
    }

    //method to create line
    private void createLine()
    {
        //create a new empty gameobject and line renderer component
        line = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
        //assign the material to the line
        line.material = material;
        //set the number of points to the line
        line.SetVertexCount(2);
        //set line color
        line.SetColors(Color.white, Color.white);
        //set the width
        line.SetWidth(0.10f, 0.10f);
        //render line to the world origin and not to the object's position
        line.useWorldSpace = true;

    }

    public void userSelectTrue()
    {
        if (!lineConnected)
        {
            lineConnected = true;
            // Button click logic here
            if (currentQuestion.isCorrect)
            {
                correct.Play(); // plays wrong sound
                score++;
            }
            else
            {
                wrong.Play();
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection
        }
    }

    public void userSelectFalse()
    {
        if (!lineConnected)
        {
            lineConnected = true;

            if (!currentQuestion.isCorrect)
            {
                correct.Play(); // plays wrong sound
                score++;

            }
            else
            {
                wrong.Play();
            }

            StartCoroutine(TransitionToNextQuestion()); // loads new question after user selection

        }
    }



}
