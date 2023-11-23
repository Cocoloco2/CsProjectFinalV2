using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayerDamaged;

    public int health = 4;
    public int maxhealth = 8;
    // Start is called before the first frame update
    void Start()
    {
       // health = maxhealth;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        OnPlayerDamaged?.Invoke();
    }
}
