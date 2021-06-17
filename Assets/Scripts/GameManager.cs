using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerHealth;

    public List<RoundData> rounds;

    public int enemyCount;
    public float spawnDelay;
    public float enemyMovementSpeed;

    public int enemiesAlive;

    public float currency = 0;

    public Transform spawnLocation;

    public GameObject enemyPrefab;
    private Enemy enemyScript;

    public TextMeshPro roundText;
    public TextMeshPro enemyText;
    public TextMeshPro healthText;
    public TextMeshPro currencyText;

    public float gameStartCountdown = 5f;
    private bool gameStart = false;
    public float startRoundCountdown = 5f;

    private bool gameRunning;

    private void Start()
    {
        StartGame();
        gameRunning = true;
        enemyScript = enemyPrefab.GetComponent<Enemy>();
    }

    private void Update()
    {
        if (playerHealth <= 0)
        {
            GameOver();
        }

        enemyScript.movementSpeed = enemyMovementSpeed;
        enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (roundText)
        {
            roundText.text = "" + rounds[0].roundNumber;
        }
        if (enemyText)
        {
            enemyText.text = "" + enemiesAlive;
        }
        if (healthText)
        {
            healthText.text = "" + playerHealth;
        }
        if (currencyText)
        {
            currencyText.text = "" + currency;
        }

        if (OVRInput.GetDown(OVRInput.Button.One) && !gameRunning)
        {
            StartGame();
            gameRunning = true;
        }

        if(gameStartCountdown <= 0 && !gameStart)
        {
            StartGame();
            gameRunning = true;
            gameStart = true;
        }

        if(gameStartCountdown > 0)
        {
            gameStartCountdown -= Time.deltaTime;
        }

        if(playerHealth <= 0)
        {
            GameOver();
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
        enemyMovementSpeed = rounds[0].enemyMoveSpeed;

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

            if (rounds[0].roundNumber == 10)
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

            Enemy enemy = Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity, null).GetComponent<Enemy>();
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
            Invoke("StartNextRound", startRoundCountdown);
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