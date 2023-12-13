using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] EnemyManager enemyManager;

    private GameEvent gameEvent = new GameEvent();
    private CollisionManager collisionManager = new CollisionManager();

    void Start()
    {
        gameEvent.Initialize(playerManager.GetPlayerPosition, collisionManager.HitCircleToCircle, enemyManager.GetEnemyDataList);
        playerManager.Initialize(gameEvent);
        enemyManager.Initialize(gameEvent);
    }

    void Update()
    {
        playerManager.OnUpdate();
        enemyManager.OnUpdate();
    }
}
