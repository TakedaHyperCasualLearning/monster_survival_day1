using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public Func<Vector2> getPlayerPosition = null;
    public Func<Vector2, Vector2, float, float, bool> checkHitCircleToCircle = null;
    public Func<List<EnemyData>> getEnemyDataList = null;

    public void Initialize(Func<Vector2> getPlayerPosition, Func<Vector2, Vector2, float, float, bool> CheckHitCircleToCircle, Func<List<EnemyData>> getEnemyDataList)
    {
        this.getPlayerPosition = getPlayerPosition;
        this.checkHitCircleToCircle = CheckHitCircleToCircle;
        this.getEnemyDataList = getEnemyDataList;
    }
}
