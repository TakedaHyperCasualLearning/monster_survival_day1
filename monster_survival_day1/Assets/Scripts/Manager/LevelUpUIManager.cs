using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUIManager : MonoBehaviour
{
    [SerializeField] private GameObject levelUpUI = null;

    private Func<bool> IsLevelUp = null;
    private Action HitPointUp = null;
    private Action AttackPointUp = null;
    private Action ShotSpeedUp = null;

    public void Initialize(GameEvent gameEvent)
    {
        IsLevelUp = gameEvent.isLevelUp;
        HitPointUp = gameEvent.hitPointUp;
        AttackPointUp = gameEvent.attackPointUp;
        ShotSpeedUp = gameEvent.shotSpeedUp;
    }

    public void OnUpdate()
    {
        if (levelUpUI.activeSelf) return;
        if (IsLevelUp())
        {
            levelUpUI.SetActive(true);
        }
    }

    public void OnHitPointUp()
    {
        HitPointUp();
        levelUpUI.SetActive(false);
    }

    public void OnAttackPointUp()
    {
        AttackPointUp();
        levelUpUI.SetActive(false);
    }

    public void OnShotSpeedUp()
    {
        ShotSpeedUp();
        levelUpUI.SetActive(false);
    }
}
