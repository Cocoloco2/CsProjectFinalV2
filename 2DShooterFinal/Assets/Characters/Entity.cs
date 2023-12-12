using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public static event Action OnDamaged;


    [SerializeField] 
    protected int health;

    [SerializeField]
    protected int maxHealth;

    [SerializeField]
    protected float moveSpeed;

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        OnDamaged?.Invoke();
    }
    public int getHealth() {  return health; }

    public int getMaxHealth() { return maxHealth;}

    public float getMoveSpeed() {  return moveSpeed; }
  }
