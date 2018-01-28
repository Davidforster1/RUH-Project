using UnityEngine;
using UnityEngine.UI;

public class year2Results : MonoBehaviour
{
    [SerializeField]
    public Text resultsText;

    public static int score;
    public static int questionsDone;

    // Use this for initialization - Stores the values 
    void Start()
    {
        score = year2GameManager.score + year2GameManager2.score2;
        questionsDone = year2GameManager.pointsAvailable + year2GameManager2.questionsDoneTwo;
        resultsText.text = "You scored " + score.ToString() + "/" + questionsDone;
    }
}