using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class year6Results : MonoBehaviour
{

    [SerializeField]
    public Text resultsText;

    public static int score, questionsDone;

    // Use this for initialization - Stores the values 
    void Start()
    {
        score = year6GameManager.score + year6GameManager2.score2 + year6GameManager3.score3 + +year6GameManager4.score4;
        questionsDone = year6GameManager.questionsDone + year6GameManager2.questionsDoneTwo + +year6GameManager3.questionsDoneThree + year6GameManager4.questionsDoneFour;
        resultsText.text = "You scored " + score.ToString() + "/" + questionsDone;
    }

}
