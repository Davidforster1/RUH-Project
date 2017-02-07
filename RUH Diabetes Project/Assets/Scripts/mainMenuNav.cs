using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuNav : MonoBehaviour {

    public void UserSelectPlay() // takes user back to the main menu
    {
        SceneManager.LoadScene("yearSelection"); // if questions done = all of them, load results screen
    }

    public void UserSelectQuit() // takes user back to the main menu
    {
        Application.Quit();
    }

    public void UserSelectYearMenu()
    {
        SceneManager.LoadScene("yearSelection");
    }

    public void UserSelectMainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void UserSelectYear1Activity()
    {
        SceneManager.LoadScene("year1Activity");
    }

    public void UserSelectYear1()
    {
        SceneManager.LoadScene("year1Menu");
    }

}
