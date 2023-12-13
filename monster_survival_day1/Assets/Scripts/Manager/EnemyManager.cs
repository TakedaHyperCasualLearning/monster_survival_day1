using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab = null;
    private List<EnemyData> enemyDataList = new List<EnemyData>();
    private Func<Vector2> GetPlayerPositionFunction = null;
    private int activeEnemyCount = 3;
    private float MOVE_SPEED = 1.0f;
    private Vector2 firstPosition = new Vector2(5.0f, 3.0f);

    public void Initialize(GameEvent gameEvent)
    {
        GetPlayerPositionFunction = gameEvent.getPlayerPosition;

        for (int i = 0; i < activeEnemyCount; i++)
        {
            GenerateEnemy(new Vector2(UnityEngine.Random.Range(-4.0f, 4.0f), UnityEngine.Random.Range(-8.0f, 8.0f)));
        }
    }

    public void OnUpdate()
    {
        for (int i = 0; i < enemyDataList.Count; i++)
        {
            EnemyData enemy = enemyDataList[i];
            VisitPlayer(enemy);
        }
    }

    public void GenerateEnemy(Vector2 position)
    {
        if (activeEnemyCount >= enemyDataList.Count)
        {
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            EnemyData enemyData = new EnemyData()
            {
                Enemy = enemy,
                MoveSpeed = MOVE_SPEED
            };
            enemyDataList.Add(enemyData);
        }
        else
        {
            EnemyData enemyData = enemyDataList[activeEnemyCount];
            enemyData.Enemy.transform.position = position;
            enemyData.Enemy.SetActive(true);
        }

    }

    private void VisitPlayer(EnemyData enemy)
    {
        Vector3 playerPosition = GetPlayerPositionFunction();
        Vector3 direction = playerPosition - enemy.Enemy.transform.position;
        enemy.Enemy.transform.up = direction;
        enemy.Enemy.transform.Translate(enemy.MoveSpeed * Time.deltaTime * Vector3.up, UnityEngine.Space.Self);
    }

}
