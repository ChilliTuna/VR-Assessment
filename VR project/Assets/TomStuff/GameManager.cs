using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public int playerHealth;

    public List<RoundData> rounds;

    public int enemyCount;
    public float spawnDelay;

    public int enemiesAlive;

    public Transform spawnLocation;

    public GameObject enemyPrefab;

    public TextMeshProUGUI roundText;
    public TextMeshProUGUI enemyText;
    public TextMeshProUGUI healthText;


    void Start()
    {
        
    }

    void Update()
    {   

        if (playerHealth <= 0)
        {
            GameOver();
        }

        roundText.text = "Current Round: " + rounds[0].roundNumber;
        enemyText.text = "Enemies Alive: " + GameObject.FindGameObjectsWithTag("Enemy").Length;
        healthText.text = "Player Health: " + playerHealth;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartNextRound();
        }

    }

    public void StartNextRound()
    {

        Debug.Log("Starting Round");

        enemyCount = rounds[0].enemyCount;
        spawnDelay = rounds[0].spawnDelay;

        InvokeRepeating("SpawnEnemy", 1f, spawnDelay);
    }

    public void GameOver()
    {

    }

    public void GameWin()
    {

    }

    public void SpawnEnemy()
    {
        if (enemyCount <= 0)
        {

            Debug.Log("No enemies left to spawn");

            CancelInvoke("SpawnEnemy");

            if (rounds.Count == 1)
            {
                Debug.LogWarning("That was the last round");
                GameWin();
            }
            else
            {
                rounds.RemoveAt(0);
            }

        }
        else
        {

            Debug.Log("Spawning Enemy");

            Instantiate(enemyPrefab, spawnLocation);
            enemyCount -= 1;
        }
    }

}
