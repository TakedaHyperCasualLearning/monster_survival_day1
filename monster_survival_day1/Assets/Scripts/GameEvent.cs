using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public Func<Vector2> getPlayerPosition = null;
    public Func<Vector2, Vector2, float, float, bool> checkHitCircleToCircle = null;
    public Func<Vector2, float, Vector2> checkHitEdge = null;
    public Func<List<EnemyData>> getEnemyDataList = null;
    public Action<EnemyData, int> hitEnemyDamage = null;
    public Action<Vector2, Vector2, int> shotBullet = null;
    public Action<int> addExperiencePoint = null;
    public Func<bool> isLevelUp = null;
    public Action hitPointUp = null;
    public Action attackPointUp = null;
    public Action shotSpeedUp = null;
    public Func<bool> GetIsGameOver = null;

    public void Initialize(
        Func<Vector2> getPlayerPosition,
        Func<Vector2, Vector2, float, float, bool> CheckHitCircleToCircle,
        Func<Vector2, float, Vector2> CheckHitEdge,
        Func<List<EnemyData>> getEnemyDataList,
        Action<EnemyData, int> hitEnemyDamage,
        Action<Vector2, Vector2, int> shotBullet,
        Action<int> addExperiencePoint,
        Func<bool> isLevelUp,
        Action hitPointUp,
        Action attackPointUp,
        Action shotSpeedUp,
        Func<bool> GetIsGameOver)
    {
        this.getPlayerPosition = getPlayerPosition;
        this.checkHitCircleToCircle = CheckHitCircleToCircle;
        this.checkHitEdge = CheckHitEdge;
        this.getEnemyDataList = getEnemyDataList;
        this.hitEnemyDamage = hitEnemyDamage;
        this.shotBullet = shotBullet;
        this.addExperiencePoint = addExperiencePoint;
        this.isLevelUp = isLevelUp;
        this.hitPointUp = hitPointUp;
        this.attackPointUp = attackPointUp;
        this.shotSpeedUp = shotSpeedUp;
        this.GetIsGameOver = GetIsGameOver;
    }
}
