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
    public Vector2 HitCircleToEdge(Vector2 position, Vector2 screenSize, float radius)
    {
        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));


        Vector2 result = new Vector2(0.0f, 0.0f);
        float left = position.x - radius;
        float right = position.x + radius;
        float top = position.y + radius;
        float bottom = position.y - radius;
        Vector2 cameraPosition = Camera.main.transform.position;

        if (left < -screenSize.x + cameraPosition.x) result += Vector2.right;
        if (right > screenSize.x + cameraPosition.x) result += Vector2.left;
        if (top > screenSize.y + cameraPosition.y) result += Vector2.down;
        if (bottom < -screenSize.y + cameraPosition.y) result += Vector2.up;

        return result.normalized;
    }
}
