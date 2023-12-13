using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    private PlayerData player;

    public void Initialize()
    {
        player = new PlayerData();
        player.Player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player.HitPoint = 100;
        player.MoveSpeed = 5.0f;
        player.CollisionSize = player.Player.gameObject.transform.localScale / 2.0f;
    }

    public void OnUpdate()
    {
        ChangeDirection();
        Move();
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
    }

    public Vector2 GetPlayerPosition()
    {
        return player.Player.transform.position;
    }
}
