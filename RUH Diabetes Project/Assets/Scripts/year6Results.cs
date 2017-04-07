using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class year6Results : MonoBehaviour
{

    [SerializeField]
    public Text resultsText;

    private int score;
    private int questionsDone;


    // Use this for initialization - Stores the values 
    void Start()
    {
        score = year5GameManager.score + year5GameManager2.score2 + year5GameManager3.score3 + +year6GameManager4.score4;
        questionsDone = year5GameManager.questionsDone + year5GameManager2.questionsDoneTwo + +year5GameManager3.questionsDoneThree + year6GameManager4.questionsDoneFour;
        resultsText.text = "You scored " + score.ToString() + "/" + questionsDone;
    }

    public void yearResetScore()
    {
        year5GameManager.score = 0;
        year5GameManager.questionsDone = 0;
        year5GameManager2.score2 = 0;
        year5GameManager2.questionsDoneTwo = 0;
        year5GameManager3.score3 = 0;
        year5GameManager3.questionsDoneThree = 0;
        year6GameManager4.score4 = 0;
        year6GameManager4.questionsDoneFour = 0;
    }

}
