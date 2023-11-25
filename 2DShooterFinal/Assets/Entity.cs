using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public static event Action OnDamaged;

    public int health;
    public int maxHealth;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        // health = maxhealth;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        OnDamaged?.Invoke();
    }
}
