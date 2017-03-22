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

    public void UserSelectYear2Activity()
    {
        SceneManager.LoadScene("year2Activity");
    }

    public void UserSelectYear2Activity2()
    {
        SceneManager.LoadScene("year2Activity2");
    }

    public void UserSelectYear3Activity()
    {
        SceneManager.LoadScene("year3Activity");
    }

    public void UserSelectYear4Activity()
    {
        SceneManager.LoadScene("year4Activity");
    }

    public void UserSelectYear5Activity()
    {
        SceneManager.LoadScene("year5Activity");
    }

    public void UserSelectYear6Activity()
    {
        SceneManager.LoadScene("year6Activity");
    }

    public void UserSelectYear1()
    {
        SceneManager.LoadScene("year1Menu");
    }

    public void UserSelectYear2()
    {
        SceneManager.LoadScene("year2Menu");
    }

    public void UserSelectYear3()
    {
        SceneManager.LoadScene("year3Menu");
    }

    public void UserSelectYear4()
    {
        SceneManager.LoadScene("year4Menu");
    }

    public void UserSelectYear5()
    {
        SceneManager.LoadScene("year5Menu");
    }

    public void UserSelectYear6()
    {
        SceneManager.LoadScene("year6Menu");
    }
}
