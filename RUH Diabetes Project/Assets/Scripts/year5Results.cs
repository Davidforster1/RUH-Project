using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class year5Results : MonoBehaviour
{

    [SerializeField]
    public Text resultsText;

    private int score;
    private int questionsDone;


    // Use this for initialization - Stores the values 
    void Start()
    {
        score = year5GameManager.score;
        questionsDone = year5GameManager.questionsDone;
        resultsText.text = "You scored " + score.ToString() + "/" + questionsDone;
    }

    public void yearResetScore()
    {
        year5GameManager.score = 0;
        year5GameManager.questionsDone = 0;
    }
}
