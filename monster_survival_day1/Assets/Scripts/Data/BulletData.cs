using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData
{
    private GameObject bullet;
    private int attackPower = 0;
    private float moveSpeed = 0.0f;
    private Vector2 velocity = Vector2.zero;
    private float radius = 0.0f;

    public GameObject Bullet { get => bullet; set => bullet = value; }
    public int AttackPower { get => attackPower; set => attackPower = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Vector2 Velocity { get => velocity; set => velocity = value; }
    public float Radius { get => radius; set => radius = value; }
}
