using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public Func<Vector2> getPlayerPosition = null;

    public void Initialize(Func<Vector2> getPlayerPosition)
    {
        this.getPlayerPosition = getPlayerPosition;
    }
}
