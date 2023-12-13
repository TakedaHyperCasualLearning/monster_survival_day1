using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] BulletManager bulletManager;
    [SerializeField] LevelUpUIManager levelUpUIManager;
    [SerializeField] GameOverUIManager gameOverUIManager;

    private GameEvent gameEvent = new GameEvent();
    private CollisionManager collisionManager = new CollisionManager();

    void Start()
    {
        gameEvent.Initialize(
            playerManager.GetPlayerPosition,
            collisionManager.HitCircleToCircle,
            collisionManager.HitCircleToEdge,
            enemyManager.GetEnemyDataList,
            enemyManager.HitDamage,
            bulletManager.ShotBullet,
            playerManager.AddExperiencePoint,
            playerManager.IsLevelUp,
            playerManager.HitPointUp,
            playerManager.AttackPointUp,
            playerManager.ShotSpeedUp,
            playerManager.IsGameOver);
        playerManager.Initialize(gameEvent);
        enemyManager.Initialize(gameEvent);
        bulletManager.Initialize(gameEvent);
        levelUpUIManager.Initialize(gameEvent);
        collisionManager.Initialize();
        gameOverUIManager.Initialize(gameEvent);
    }

    void Update()
    {
        playerManager.OnUpdate();
        enemyManager.OnUpdate();
        bulletManager.OnUpdate();
        levelUpUIManager.OnUpdate();
        gameOverUIManager.OnUpdate();
    }
}
