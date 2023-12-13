using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] EnemyManager enemyManager;

    private GameEvent gameEvent = new GameEvent();

    void Start()
    {
        gameEvent.Initialize(playerManager.GetPlayerPosition);
        playerManager.Initialize();
        enemyManager.Initialize(gameEvent);
    }

    void Update()
    {
        playerManager.OnUpdate();
        enemyManager.OnUpdate();
    }
}
