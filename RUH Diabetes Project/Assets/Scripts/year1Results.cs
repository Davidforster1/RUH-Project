using UnityEngine;
using UnityEngine.UI;

public class year1Results : MonoBehaviour
{
    [SerializeField]
    public Text resultsText;

    private int score;
    private int questionsDone;

    // Use this for initialization - Stores the values 
    void Start()
    {
        score = year1GameManager.score; 
        questionsDone = year1GameManager.questionsDone; 
        resultsText.text = "You scored " + score.ToString() + "/" + questionsDone; // shows the score on the results screen
    }
}
