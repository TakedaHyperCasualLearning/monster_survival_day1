using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI = null;

    private Func<bool> IsGameOver = null;

    public void Initialize(GameEvent gameEvent)
    {
        IsGameOver = gameEvent.GetIsGameOver;
    }

    public void OnUpdate()
    {
        if (gameOverUI.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
            }
        }
        else if (IsGameOver())
        {
            gameOverUI.SetActive(true);
        }


    }
}
