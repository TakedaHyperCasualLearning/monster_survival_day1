using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private GameObject player;
    private int hitPoint = 0;
    private float moveSpeed = 0.0f;
    private Vector2 collisionSize = Vector2.zero;


    public GameObject Player { get => player; set => player = value; }
    public int HitPoint { get => hitPoint; set => hitPoint = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Vector2 CollisionSize { get => collisionSize; set => collisionSize = value; }
}
