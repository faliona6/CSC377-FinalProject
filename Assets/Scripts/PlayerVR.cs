using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerVR : MonoBehaviour
{
    public int startingHealth;

    public Action deathEvent;

    private int health;

    void Start()
    {
        health = startingHealth;
    }

    public void changeHealth(int deltaHealth)
    {
        health += deltaHealth;
        if (health < 0)
            deathEvent?.Invoke();
    }
}
