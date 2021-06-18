using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win_LossCondition : MonoBehaviour
{
    public Canvas loseCanvas;
    public Canvas victoryCanvas;
    
    public void LossSetup()
    {
        loseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void VictorySetup()
    {
        victoryCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main_Game_Scene");
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }
}
