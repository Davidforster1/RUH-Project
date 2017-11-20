using UnityEngine;
using UnityEngine.UI;

public class year5Results : MonoBehaviour
{
    [SerializeField]
    public Text resultsText;

    public static int score;
    public static int questionsDone;


    // Use this for initialization - Stores the values 
    void Start()
    {
        score = year5GameManager.score + year5GameManager2.score2 + year5GameManager3.score3;
        questionsDone = year5GameManager.questionsDone + year5GameManager2.questionsDoneTwo + year5GameManager3.questionsDoneThree;
        resultsText.text = "You scored " + score.ToString() + "/" + questionsDone;
    }
}
