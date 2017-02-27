using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class year1GameManager : MonoBehaviour {

    public year1Quiz[] imagePanel;
    private static List<year1Quiz> unansweredQuestions;
    private year1Quiz currentQuestion;

    [SerializeField]
    Image questionImage; // question text

    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = imagePanel.ToList<year1Quiz>();
        }

        SetRandomImage();
        Debug.Log(currentQuestion.image + " is " + currentQuestion.isCorrect);
    }   
    void SetRandomImage()
    {
        int randomImageIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomImageIndex];

        questionImage = currentQuestion.image; 

        unansweredQuestions.RemoveAt(randomImageIndex);   
    }
}
