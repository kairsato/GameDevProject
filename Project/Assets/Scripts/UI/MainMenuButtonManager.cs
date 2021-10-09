using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour


{

    // Defines the main menu, options, and credits objects which are enabled/disabled based what buttons are pressed
    public GameObject main;

    public GameObject options;

    public GameObject team;

    public GameObject credits;

    void start ()
    {


    }

    void update()
    {


    }



    public void LoadGame(string level) // Load level button
    {
        SceneManager.LoadScene(level);
    }

    public void quitGame() // Quit game button
    {
        Application.Quit();
    }

    public void showOptions()
    {
        main.SetActive(false);
    }
    

    public void showCredits()
    {

    }


    public void showTeam()
    {


    }


    public void goBacktoMain()
    {


    }



}
