using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject cameraObject;
    [SerializeField] TextMeshProUGUI hitPointText;
    private PlayerData player;
    private Func<List<EnemyData>> GetEnemyDataList = null;
    private Func<Vector2, Vector2, float, float, bool> CheckHitCircleToCircle = null;
    private Action<Vector2, Vector2, int> ShotBullet = null;
    private float hitCoolTime = 0.0f;
    private float HIT_COOL_TIME = 0.5f;
    private float shotTimer = 0.0f;
    private float SHOT_INTERVAL = 0.5f;
    private int LEVELUP_BORDER = 3;
    private int LEVELUP_ADD_HIT_POINT = 10;
    private float LEVELUP_SUB_SHOT_INTERVAL = 0.05f;
    private int LEVELUP_ADD_ATTACK_POWER = 1;
    private float hitPointTextOffset = 0.5f;
    private bool isLevelUp = false;
    private bool isGameOver = false;

    public void Initialize(GameEvent gameEvent)
    {
        GetEnemyDataList = gameEvent.getEnemyDataList;
        CheckHitCircleToCircle = gameEvent.checkHitCircleToCircle;
        ShotBullet = gameEvent.shotBullet;

        GameObject playerObject = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player = new PlayerData()
        {
            Player = playerObject,
            HitPoint = 100,
            HitPointMax = 100,
            MoveSpeed = 5.0f,
            AttackPower = 1,
            CollisionSize = playerObject.transform.localScale / 2.0f
        };

        hitPointText.text = "HP: " + player.HitPoint + "/" + player.HitPointMax;
        hitPointText.transform.position = player.Player.transform.position + new Vector3(0.0f, hitPointTextOffset, 0.0f);
    }

    public void OnUpdate()
    {
        if (isLevelUp) { return; }
        if (player.Player.activeSelf == false) { return; }

        ChangeDirection();
        Move();

        if (shotTimer < SHOT_INTERVAL) { shotTimer += Time.deltaTime; }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePosition = Input.mousePosition;
                Vector2 direction = Camera.main.ScreenToWorldPoint(mousePosition) - player.Player.transform.position;
                direction.Normalize();
                ShotBullet(player.Player.transform.position, direction, player.AttackPower);
                shotTimer = 0.0f;
            }
        }

        if (hitCoolTime < HIT_COOL_TIME)
        {
            hitCoolTime += Time.deltaTime;
            return;
        }
        HitEnemy();
    }

    private void ChangeDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10.0f;
        Vector3 direction = Camera.main.ScreenToWorldPoint(mousePosition) - player.Player.transform.position;
        player.Player.transform.up = direction;
    }

    private void Move()
    {
        Vector2 velocity = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) { velocity.y += 1.0f; }
        if (Input.GetKey(KeyCode.S)) { velocity.y -= 1.0f; }
        if (Input.GetKey(KeyCode.A)) { velocity.x -= 1.0f; }
        if (Input.GetKey(KeyCode.D)) { velocity.x += 1.0f; }

        player.Player.transform.Translate(velocity * player.MoveSpeed * Time.deltaTime, UnityEngine.Space.Self);
        cameraObject.transform.position = player.Player.transform.position - new Vector3(0.0f, 0.0f, 10.0f);
        hitPointText.transform.position = player.Player.transform.position + new Vector3(0.0f, hitPointTextOffset, 0.0f);
    }

    private void HitEnemy()
    {
        List<EnemyData> enemyDataList = GetEnemyDataList();
        for (int i = 0; i < enemyDataList.Count; i++)
        {
            EnemyData enemy = enemyDataList[i];
            if (enemy.Enemy.activeSelf == false) { continue; }
            if (CheckHitCircleToCircle(player.Player.transform.position, enemy.Enemy.transform.position, player.CollisionSize.x, enemy.CollisionSize.x))
            {
                Damage(enemy.AttackPower);
            }
        }
    }

    private void Damage(int damage)
    {
        player.HitPoint -= damage;
        hitCoolTime = 0.0f;
        hitPointText.text = "HP: " + player.HitPoint + "/" + player.HitPointMax;
        if (player.HitPoint <= 0)
        {
            player.HitPoint = 0;
            hitPointText.text = "HP: " + player.HitPoint + "/" + player.HitPointMax;
            player.Player.SetActive(false);
            isGameOver = true;
        }
    }

    public Vector2 GetPlayerPosition()
    {
        return player.Player.transform.position;
    }

    public void AddExperiencePoint(int point)
    {
        player.ExperiencePoint += point;
        if (player.ExperiencePoint >= LEVELUP_BORDER)
        {
            isLevelUp = true;
            player.ExperiencePoint -= LEVELUP_BORDER;
        }
    }

    public bool IsLevelUp()
    {
        if (isLevelUp) return true;
        if (player.ExperiencePoint >= LEVELUP_BORDER)
        {
            isLevelUp = true;
            player.ExperiencePoint -= LEVELUP_BORDER;
            return true;
        }

        return false;
    }

    public void HitPointUp()
    {
        player.HitPointMax += LEVELUP_ADD_HIT_POINT;
        player.HitPoint += LEVELUP_ADD_HIT_POINT;
        hitPointText.text = "HP: " + player.HitPoint + "/" + player.HitPointMax;
        isLevelUp = false;
    }

    public void AttackPointUp()
    {
        player.AttackPower += LEVELUP_ADD_ATTACK_POWER;
        isLevelUp = false;
    }

    public void ShotSpeedUp()
    {
        SHOT_INTERVAL -= LEVELUP_SUB_SHOT_INTERVAL;
        isLevelUp = false;
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }
}
