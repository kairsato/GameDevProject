using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour


{

 

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
