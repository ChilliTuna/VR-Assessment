using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Round Data", menuName = "VR Project/Create Round Data", order = 1)]
public class RoundData : ScriptableObject
{
    public int roundNumber;
    public int enemyCount;
    public float spawnDelay;
    public float enemyMoveSpeed;
}
