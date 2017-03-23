using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class year2Results : MonoBehaviour
{

    [SerializeField]
    public Text resultsText;

    private int score;
    private int questionsDone;


    // Use this for initialization - Stores the values 
    void Start()
    {
        score = year2GameManager.score + year2GameManager2.score2;
        questionsDone = year2GameManager.questionsDoneOne + year2GameManager2.questionsDoneTwo;
        resultsText.text = "You scored " + score.ToString() + "/" + questionsDone;
    }

    public void yearResetScore()
    {
        year2GameManager.score = 0;
        year2GameManager2.score2 = 0;
        year2GameManager.questionsDoneOne = 0;
        year2GameManager2.questionsDoneTwo = 0;
    }
}