using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager
{
    private Vector2 baseScreenSize = Vector2.zero;

    public void Initialize()
    {
        baseScreenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
    public bool HitCircleToCircle(Vector2 position1, Vector2 position2, float radius1, float radius2)
    {
        Vector2 distance = position1 - position2;
        float distanceLength = distance.magnitude;
        float hitLength = radius1 + radius2;

        if (distanceLength <= hitLength) return true;
        return false;
    }
    public Vector2 HitCircleToEdge(Vector2 position, float radius)
    {
        Vector2 result = new Vector2(0.0f, 0.0f);
        float left = position.x - radius;
        float right = position.x + radius;
        float top = position.y + radius;
        float bottom = position.y - radius;
        Vector2 cameraPosition = Camera.main.transform.position;

        if (left < cameraPosition.x - baseScreenSize.x) result += Vector2.right;
        if (right > cameraPosition.x + baseScreenSize.x) result += Vector2.left;
        if (top > cameraPosition.y + baseScreenSize.y) result += Vector2.down;
        if (bottom < cameraPosition.y - baseScreenSize.y) result += Vector2.up;

        return result.normalized;
    }
}
