using UnityEngine;
using UnityEngine.UI;

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
}
