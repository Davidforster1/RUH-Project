using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class year3Results : MonoBehaviour
{

    [SerializeField]
    public Text resultsText;

    private int score;
    private int questionsDone;


    // Use this for initialization - Stores the values 
    void Start()
    {
        score = year3GameManager.score;
        questionsDone = year3GameManager.questionsDone;
        resultsText.text = "You scored " + score.ToString() + "/" + questionsDone;
    }

    public void yearResetScore()
    {
        year3GameManager.score = 0;
        year3GameManager.questionsDone = 0;
    }
}
