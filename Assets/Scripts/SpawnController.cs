using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject enemy;
     
    private Vector3 spawnPoint;
    private float randomX;
    private float randomY;

    public float timeBetweenSpawns;
    private float timeBetweenSpawnsCount;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = enemy.transform.position;
        //spawnPoint = new Vector3 (-7f, 3f, -30f);
        timeBetweenSpawnsCount = timeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBetweenSpawnsCount <= 0)
        {
            randomX = Random.Range(-20, 20);
            randomY = Random.Range(-20, 20);
            spawnPoint.x = randomX;
            spawnPoint.y = randomY;
            Instantiate(enemy, spawnPoint, Quaternion.identity);
            timeBetweenSpawnsCount = timeBetweenSpawns;
        }
        else
        {
            timeBetweenSpawnsCount -= Time.deltaTime;
        }
        
    }
}
