using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameLoopManager : MonoBehaviour
{
    private static GameLoopManager _instance;
    public static GameLoopManager Instance { get { return _instance; } }

    public PlayerVR player;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        player.deathEvent += () => OnPlayerVRDeath();
    }

    public void PlayerHit()
    {

    }

    private void OnPlayerVRDeath()
    {

    }


}
