using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarsGameManager : MonoBehaviour
{
    public List<EnemyHealth> enemies;

    void Start()
    {
        enemies = FindObjectsOfType<EnemyHealth>().ToList();
    }

    public void AddNewEnemy()
    { 
    
    }


    void ResetGame()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].ResetEnemy();
        }
    }
}
