using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private GameObject player;
    private int hitPoint = 0;
    private int hitPointMax = 0;
    private float moveSpeed = 0.0f;
    private Vector2 collisionSize = Vector2.zero;
    private int experiencePoint = 0;
    private int attackPower = 0;


    public GameObject Player { get => player; set => player = value; }
    public int HitPoint { get => hitPoint; set => hitPoint = value; }
    public int HitPointMax { get => hitPointMax; set => hitPointMax = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Vector2 CollisionSize { get => collisionSize; set => collisionSize = value; }
    public int ExperiencePoint { get => experiencePoint; set => experiencePoint = value; }
    public int AttackPower { get => attackPower; set => attackPower = value; }
}
