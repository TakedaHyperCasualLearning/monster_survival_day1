using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager
{
    public bool HitCircleToCircle(Vector2 position1, Vector2 position2, float radius1, float radius2)
    {
        Vector2 distance = position1 - position2;
        float distanceLength = distance.magnitude;
        float hitLength = radius1 + radius2;

        if (distanceLength < hitLength) return true;
        else return false;
    }
}
