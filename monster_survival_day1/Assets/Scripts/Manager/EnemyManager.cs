using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab = null;
    private List<EnemyData> enemyDataList = new List<EnemyData>();
    private Func<Vector2> GetPlayerPositionFunction = null;
    private Action<int> AddExperiencePointFunction = null;
    private Func<bool> GetIsLevelUpFunction = null;
    private int activeEnemyCount = 3;
    private float MOVE_SPEED = 1.0f;
    private int HIT_POINT = 5;
    private int ATTACK_POWER = 5;
    private Vector2 firstPosition = new Vector2(5.0f, 3.0f);
    private float spawnTime = 0.0f;
    private float SPAWN_INTERVAL = 3.0f;
    private Vector2 screenSize = Vector2.zero;
    private Vector2 spawnPositionOffset = Vector2.zero;
    private int EXPERIENCE_POINT = 1;

    public void Initialize(GameEvent gameEvent)
    {
        GetPlayerPositionFunction = gameEvent.getPlayerPosition;
        AddExperiencePointFunction = gameEvent.addExperiencePoint;
        GetIsLevelUpFunction = gameEvent.isLevelUp;

        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        for (int i = 0; i < 3; i++)
        {
            GenerateEnemy(new Vector2(UnityEngine.Random.Range(-4.0f, 4.0f), UnityEngine.Random.Range(-8.0f, 8.0f)));
        }
    }

    public void OnUpdate()
    {
        if (GetIsLevelUpFunction()) { return; }

        for (int i = 0; i < enemyDataList.Count; i++)
        {
            EnemyData enemy = enemyDataList[i];
            if (enemy.Enemy.activeSelf == false) { continue; }
            VisitPlayer(enemy);
        }

        if (spawnTime < SPAWN_INTERVAL) { spawnTime += Time.deltaTime; }
        else
        {
            Vector2 randomOffset = new Vector2(UnityEngine.Random.Range(screenSize.x, screenSize.x + spawnPositionOffset.x), UnityEngine.Random.Range(screenSize.y, screenSize.y + spawnPositionOffset.y));
            randomOffset *= UnityEngine.Random.Range(0, 2) == 0 ? 1 : -1;
            Vector2 spawnPosition = GetPlayerPositionFunction() + randomOffset;
            GenerateEnemy(spawnPosition);
            spawnTime = 0.0f;
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
                HitPoint = HIT_POINT,
                AttackPower = ATTACK_POWER,
                MoveSpeed = MOVE_SPEED,
                CollisionSize = enemy.transform.localScale / 2.0f,
                ExperiencePoint = EXPERIENCE_POINT
            };
            enemyDataList.Add(enemyData);
        }
        else
        {
            EnemyData enemyData = GetNotUseEnemy();
            enemyData.Enemy.transform.position = position;
            enemyData.HitPoint = HIT_POINT;
            enemyData.AttackPower = ATTACK_POWER;
            enemyData.MoveSpeed = MOVE_SPEED;
            enemyData.CollisionSize = enemyData.Enemy.transform.localScale / 2.0f;
            enemyData.Enemy.SetActive(true);
        }
        activeEnemyCount++;
    }

    private EnemyData GetNotUseEnemy()
    {
        for (int i = 0; i < enemyDataList.Count; i++)
        {
            EnemyData enemy = enemyDataList[i];
            if (!enemy.Enemy.activeSelf) { return enemy; }
        }
        return null;
    }

    public void HitDamage(EnemyData enemy, int damage)
    {
        enemy.HitPoint -= damage;
        if (enemy.HitPoint <= 0)
        {
            enemy.Enemy.SetActive(false);
            AddExperiencePointFunction(enemy.ExperiencePoint);
            activeEnemyCount--;
        }
    }

    private void VisitPlayer(EnemyData enemy)
    {
        Vector3 playerPosition = GetPlayerPositionFunction();
        Vector3 direction = playerPosition - enemy.Enemy.transform.position;
        enemy.Enemy.transform.up = direction;
        enemy.Enemy.transform.Translate(enemy.MoveSpeed * Time.deltaTime * Vector3.up, UnityEngine.Space.Self);
    }

    public List<EnemyData> GetEnemyDataList()
    {
        return enemyDataList;
    }

}
