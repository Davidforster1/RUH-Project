using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class year1Results : MonoBehaviour {

    [SerializeField]
    public Text resultsText;

    private int score;
    private int questionsDone;           


	// Use this for initialization
	void Start () {
        score = year1GameManager.score;
        questionsDone = year1GameManager.questionsDone;
        resultsText.text = "Your facking score was " + score.ToString() + "/" + questionsDone;
	}

}
