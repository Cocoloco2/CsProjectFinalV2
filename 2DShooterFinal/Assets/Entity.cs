using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public static event Action OnDamaged;

    protected int health;
    protected int maxHealth;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        // health = maxhealth;
    }

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        OnDamaged?.Invoke();
    }
    public virtual int getMaxHealth() {  return maxHealth; }

    public virtual int GetHealth() { return health; }

    public virtual float GetMoveSpeed() {  return moveSpeed; }
}
