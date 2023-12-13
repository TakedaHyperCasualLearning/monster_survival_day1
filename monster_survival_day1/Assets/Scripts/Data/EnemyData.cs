using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    private GameObject enemy;
    private int hitPoint = 0;
    private int attackPower = 0;
    private float moveSpeed = 0.0f;
    private Vector2 velocity = Vector2.zero;
    private Vector2 collisionSize = Vector2.zero;

    public GameObject Enemy { get => enemy; set => enemy = value; }
    public int HitPoint { get => hitPoint; set => hitPoint = value; }
    public int AttackPower { get => attackPower; set => attackPower = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Vector2 Velocity { get => velocity; set => velocity = value; }
    public Vector2 CollisionSize { get => collisionSize; set => collisionSize = value; }

}
