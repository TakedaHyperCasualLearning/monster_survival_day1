using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    private List<BulletData> bulletDataList = new List<BulletData>();
    private Func<Vector2, float, Vector2> checkHitEdge = null;
    private Func<Vector2, Vector2, float, float, bool> checkHitCircleToCircle = null;
    private Func<List<EnemyData>> getEnemyDataList = null;
    private Action<EnemyData, int> hitEnemyDamage = null;
    private Func<bool> GetIsLevelUp = null;
    private float bulletSpeed = 5.0f;
    private int activeBulletCount = 0;

    public void Initialize(GameEvent gameEvent)
    {
        checkHitEdge = gameEvent.checkHitEdge;
        checkHitCircleToCircle = gameEvent.checkHitCircleToCircle;
        getEnemyDataList = gameEvent.getEnemyDataList;
        hitEnemyDamage = gameEvent.hitEnemyDamage;
        GetIsLevelUp = gameEvent.isLevelUp;
    }

    public void OnUpdate()
    {
        if (GetIsLevelUp()) { return; }

        for (int i = 0; i < bulletDataList.Count; i++)
        {
            BulletData bulletData = bulletDataList[i];

            if (!bulletData.Bullet.activeSelf) continue;

            MoveBullet(bulletData);
            HitCollision(bulletData);
        }
    }

    private void MoveBullet(BulletData bulletData)
    {
        bulletData.Bullet.transform.Translate(bulletData.Velocity * Time.deltaTime);
    }

    public void ShotBullet(Vector2 position, Vector2 direction, int attackPower)
    {
        CreateBullet(position, direction, attackPower);
    }

    private void CreateBullet(Vector2 position, Vector2 direction, int attackPower)
    {
        if (activeBulletCount >= bulletDataList.Count)
        {
            GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
            BulletData bulletData = new BulletData()
            {
                Bullet = bullet,
                AttackPower = attackPower,
                MoveSpeed = bulletSpeed,
                Velocity = direction * bulletSpeed,
                Radius = bullet.transform.localScale.x / 2.0f,
            };
            bulletDataList.Add(bulletData);
        }
        else
        {
            BulletData bulletData = GetNotUseBullet();
            bulletData.Bullet.transform.position = position;
            bulletData.AttackPower = attackPower;
            bulletData.MoveSpeed = bulletSpeed;
            bulletData.Velocity = direction * bulletSpeed;
            bulletData.Bullet.SetActive(true);
        }

        activeBulletCount++;
    }

    private BulletData GetNotUseBullet()
    {
        for (int i = 0; i < bulletDataList.Count; i++)
        {
            BulletData bulletData = bulletDataList[i];
            if (!bulletData.Bullet.activeSelf) return bulletData;
        }
        return null;
    }

    private void HitCollision(BulletData bullet)
    {
        Vector2 hitDirection = checkHitEdge(bullet.Bullet.transform.position, -bullet.Radius);
        if (hitDirection != Vector2.zero)
        {
            bullet.Bullet.SetActive(false);
            activeBulletCount--;
            return;
        }

        List<EnemyData> enemyDataList = getEnemyDataList();
        for (int i = 0; i < enemyDataList.Count; i++)
        {
            EnemyData enemy = enemyDataList[i];
            if (enemy.Enemy.activeSelf == false) continue;
            if (checkHitCircleToCircle(bullet.Bullet.transform.position, enemy.Enemy.transform.position, bullet.Radius, enemy.CollisionSize.x))
            {
                Debug.Log("radius: " + bullet.Radius + ", enemy CollisionSize: " + enemy.CollisionSize.x);
                bullet.Bullet.SetActive(false);
                activeBulletCount--;
                hitEnemyDamage(enemy, bullet.AttackPower);
                return;
            }
        }

    }

    public List<BulletData> GetBulletDataList()
    {
        return bulletDataList;
    }
}
