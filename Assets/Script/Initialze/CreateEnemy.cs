using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyGround;
    void Start()
    {
        Create();
    }
    public void Create()
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = enemyGround.transform.position;
        enemy.GetComponent<Enemy>().row = enemyGround.GetComponent<Ground>().row;
        enemy.GetComponent<Enemy>().column = enemyGround.GetComponent<Ground>().column;
        enemyGround.GetComponent<Ground>().isHaveObject = true;
        enemyGround.GetComponent<Ground>().objectControl = enemy;
    }
}
