﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuNav : MonoBehaviour {

    [SerializeField]
    public GameObject helpCanvas;

    [SerializeField]
    private GameObject wifiAlertCanvas;

    [SerializeField]
    private GameObject mainMenuCanvas;

    [SerializeField]
    private GameObject activityCanvas; // the canvas which holds the activity

    [SerializeField]
    private GameObject escapeCanvas; // the canvas which asks the user for quit and email confirmation

    public void UserSelectPlay() // takes user back to the main menu
    {
        SceneManager.LoadScene("yearSelection"); // if questions done = all of them, load results screen
    }

    public void UserSelectQuit() // takes user back to the main menu
    {
        Application.Quit();
    }

    public void UserStartVideo()
    {
        SceneManager.LoadScene("mainMenuVideo");
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

    public void UserSelectYear1Results1()
    {
        SceneManager.LoadScene("year1Results");
    }

    public void UserSelectYear1Results2()
    {
        SceneManager.LoadScene("year1Results2");
    }

    public void UserSelectYear2Activity()
    {
        SceneManager.LoadScene("year2Activity");
    }

    public void UserSelectYear2Activity2()
    {
        SceneManager.LoadScene("year2Activity2");
    }

    public void UserSelectYear2Results1()
    {
        SceneManager.LoadScene("year2Results");
    }

    public void UserSelectYear2Results2()
    {
        SceneManager.LoadScene("year2Results2");
    }

    public void UserSelectYear3Activity()
    {
        SceneManager.LoadScene("year3Activity");
    }

    public void UserSelectYear3Results1()
    {
        SceneManager.LoadScene("year3Results");
    }

    public void UserSelectYear3Results2()
    {
        SceneManager.LoadScene("year3Results2");
    }

    public void UserSelectYear4Activity()
    {
        SceneManager.LoadScene("year4Activity");
    }

    public void UserSelectYear4Results1()
    {
        SceneManager.LoadScene("year4Results");
    }

    public void UserSelectYear4Results2()
    {
        SceneManager.LoadScene("year4Results2");
    }

    public void UserSelectYear5Activity()
    {
        SceneManager.LoadScene("year5Activity");
    }

    public void UserSelectYear5Activity2()
    {
        SceneManager.LoadScene("year5Activity2");
    }

    public void UserSelectYear5Activity3()
    {
        SceneManager.LoadScene("year5Activity3");
    }

    public void UserSelectYear5Results1()
    {
        SceneManager.LoadScene("year5Results");
    }

    public void UserSelectYear5Results2()
    {
        SceneManager.LoadScene("year5Results2");
    }

    public void UserSelectYear6Activity()
    {
        SceneManager.LoadScene("year6Activity");
    }

    public void UserSelectYear6Activity2()
    {
        SceneManager.LoadScene("year6Activity2");
    }

    public void UserSelectYear6Activity3()
    {
        SceneManager.LoadScene("year6Activity3");
    }

    public void UserSelectYear6Activity4()
    {
        SceneManager.LoadScene("year6Activity4");
    }

    public void UserSelectYear6Results1()
    {
        SceneManager.LoadScene("year6Results");
    }

    public void UserSelectYear6Results2()
    {
        SceneManager.LoadScene("year6Results2");
    }

    public void UserSelectYear1()
    {
        SceneManager.LoadScene("year1Menu1");
    }

    public void UserSelectYear1Two()
    {
        SceneManager.LoadScene("year1Menu1.1");
    }

    public void UserSelectYear2()
    {
        SceneManager.LoadScene("year2Menu1");
    }

    public void UserSelectYear2point1()
    {
        SceneManager.LoadScene("year2Menu1.1");
    }

    public void UserSelectYear3()
    {
        SceneManager.LoadScene("year3Menu1");
    }

    public void UserSelectYear3point1()
    {
        SceneManager.LoadScene("year3Menu1.1");
    }

    public void UserSelectYear4()
    {
        SceneManager.LoadScene("year4Menu1");
    }

    public void UserSelectYear4point1()
    {
        SceneManager.LoadScene("year4Menu1.1");
    }

    public void UserSelectYear5()
    {
        SceneManager.LoadScene("year5Menu1");
    }

    public void UserSelectYear5point1()
    {
        SceneManager.LoadScene("year5Menu1.1");
    }

    public void UserSelectYear6()
    {
        SceneManager.LoadScene("year6Menu1");
    }

    public void UserSelectYear6point1()
    {
        SceneManager.LoadScene("year6Menu1.1");
    }

    public void ToggleWifiWarning() // hides main menu canvas, enables wifi warning 
    {
            mainMenuCanvas.SetActive(false);
            wifiAlertCanvas.SetActive(true);
    }

    public void MainMenuReset()
    {
        mainMenuCanvas.SetActive(true);
        wifiAlertCanvas.SetActive(false);
    }

    public void ToggleEscapeWarning() // hides main menu canvas, enables wifi warning 
    {
        if (activityCanvas.activeSelf == true && escapeCanvas.activeSelf == false)
        {
            activityCanvas.SetActive(false);
            escapeCanvas.SetActive(true);
        }
        else
        {
            activityCanvas.SetActive(true);
            escapeCanvas.SetActive(false);
        }
    }


    public void ToggleHelpScreen() // toggles help text
    {
        if (helpCanvas.activeSelf == false)
        {
            helpCanvas.SetActive(true);
        }
        else
        {
            helpCanvas.SetActive(false);
        }
    }
    
    public void DisableHelpScreen()
    {
        helpCanvas.SetActive(false);
    }

}
