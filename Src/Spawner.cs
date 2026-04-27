using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public float waveDelay = 2;
    float countDown;
    int waveNo = 1;

    public GameObject[] EnemyPrefab;
    public Transform enemyStartPoint;

    public bool gameOver = false;
    void Start()
    {
        StartCoroutine(WaveSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        //countDown -= Time.deltaTime;
        //if (countDown < 0)
        //{
        //    StartCoroutine(WaveSpawn());
        //    countDown = waveDelay;
        //}
    }
    IEnumerator WaveSpawn()
    {
        float enemyDelay = 0.7f;
        while (!gameOver)
        {          
            if (enemyDelay > 0.1f)
            {
                enemyDelay -= 0.01f;
            }
            waveNo++;
            for (int i = 0; i < waveNo; i++)
            {
                Instantiate(EnemyPrefab[0], enemyStartPoint.position, Quaternion.identity);

                yield return new WaitForSeconds(enemyDelay);            
            }
            if (waveNo > 5)
            {
                for (int i = 0; i < waveNo - 5; i++)
                {
                   GameObject enemyGo = Instantiate(EnemyPrefab[1], enemyStartPoint.position, Quaternion.identity);
                    Enemy enemy = enemyGo.GetComponent<Enemy>();
                    enemy.hp += waveNo;
                    enemy.speed += waveNo;
                    yield return new WaitForSeconds(enemyDelay);
                }
            }
            yield return new WaitForSeconds(waveDelay);
        }
    }  
}
