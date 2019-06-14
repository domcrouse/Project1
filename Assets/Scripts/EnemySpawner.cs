using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Dimitrios Martin - MAR16003880
Games Technologies
Code For Project Re-Imagined*/
public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    float RandomHorizontal;
    Vector2 Spawnpoint;
    public float SpawnRate = 2f;
    float SpawnNextenemy  = 0.0f;

    //spawns enemy at a set amount of time at random positions
    void Update()
    {
        if (Time.time > SpawnNextenemy)
        {
            SpawnNextenemy = Time.time + SpawnRate;
            RandomHorizontal = Random.Range(-8.4f, 8.4f);
            Spawnpoint = new Vector2(RandomHorizontal, transform.position.y);
            Instantiate(Enemy, Spawnpoint, Quaternion.identity);
        }
    }
}
