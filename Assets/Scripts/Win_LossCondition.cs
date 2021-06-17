using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win_LossCondition : MonoBehaviour
{
    public Sprite gameOverUI;
    public Sprite victoryUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseGame()
    {
        SceneManager.LoadScene("LoseScene");
    }

    public void WinGame()
    {
        SceneManager.LoadScene("VictoryScene");
    }
}
