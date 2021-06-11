using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{

    public int playerHealth;

    public List<RoundData> rounds;

    public int enemyCount;
    public float spawnDelay;

    public int enemiesAlive;

    public float currency = 0;

    public Transform spawnLocation;

    public GameObject enemyPrefab;

    public TextMeshPro roundText;
    public TextMeshPro enemyText;
    public TextMeshPro healthText;
    public TextMeshPro currencyText;

    bool gameRunning;


    void Start()
    {
        
    }

    void Update()
    {   

        if (playerHealth <= 0)
        {
            GameOver();
        }

        enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;

        roundText.text = "Current Round: " + rounds[0].roundNumber;
        enemyText.text = "Enemies Alive: " + enemiesAlive;
        healthText.text = "Player Health: " + playerHealth;
        currencyText.text = "Points: " + currency;


        if (OVRInput.GetDown(OVRInput.Button.One) && !gameRunning)
        {
            StartGame();
            gameRunning = true;
        }

    }

    public void StartGame()
    {
        Invoke("StartNextRound", 3);
    }

    public void StartNextRound()
    {

        Debug.Log("Starting Round " + rounds[0].roundNumber);

        enemyCount = rounds[0].enemyCount;
        spawnDelay = rounds[0].spawnDelay;

        InvokeRepeating("SpawnEnemy", 1f, spawnDelay);
    }

    public void GameOver()
    {
        Debug.LogWarning("Game is over");
    }

    public void GameWin()
    {
        Debug.LogWarning("You have won");
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

                InvokeRepeating("WaitForEnd", 0f, 1f);

            }
            else
            {
                rounds.RemoveAt(0);

                InvokeRepeating("WaitForClearLevel", 0f, 1f);
            }

        }
        else
        {

            Debug.Log("Spawning Enemy");

            Enemy enemy =  Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity, null).GetComponent<Enemy>();
            enemy.onDeath.AddListener(() => AddCurrency(enemy.value));

            enemyCount -= 1;
        }
    }

    public void AddCurrency(float amount)
    {
        currency += amount;
    }

    public void WaitForClearLevel()
    {
        if (enemiesAlive <= 0)
        {
            CancelInvoke("WaitForClearLevel");
            Invoke("StartNextRound", 5);
            
        }
    }

    public void WaitForEnd()
    {
        if (playerHealth <= 0)
        {
            CancelInvoke("WaitForEnd");

            GameOver();
        }
        else if (enemiesAlive <= 0)
        {
            CancelInvoke("WaitForEnd");

            GameWin();
        }
        else
        {
            Debug.Log("Waiting for all enemies to die or for player to die");
        }


    }

}
