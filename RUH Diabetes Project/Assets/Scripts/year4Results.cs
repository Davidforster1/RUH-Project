using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class year4Results : MonoBehaviour {

    [SerializeField]
    public Text resultsText;

    private int score;
    private int questionsDone;


    // Use this for initialization - Stores the values 
    void Start()
    {
        score = year4GameManager.score;
        questionsDone = year4GameManager.questionsDone;
        resultsText.text = "You scored " + score.ToString() + "/" + questionsDone;
    }

    public void yearResetScore()
    {
        year4GameManager.score = 0;
        year4GameManager.questionsDone = 0;
    }

}
