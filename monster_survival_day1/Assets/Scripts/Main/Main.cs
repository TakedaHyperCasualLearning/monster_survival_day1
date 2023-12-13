using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    void Start()
    {
        playerManager.Initialize();
    }

    void Update()
    {
        playerManager.OnUpdate();
    }
}
