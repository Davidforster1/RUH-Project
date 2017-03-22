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
        score = year2GameManager2.score;
        questionsDone = year2GameManager.questionsDoneOne + year2GameManager2.questionsDoneTwo;
        resultsText.text = "You scored " + score.ToString() + "/" + questionsDone;
    }

}