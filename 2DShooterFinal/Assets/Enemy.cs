using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    public GameObject player;
    private float distanceToPlayer;
    private void Update()
    {
        trackPlayer();
        if (health <= 0) { 
            Destroy(gameObject);
            }
        Debug.Log(health);
    }
    void trackPlayer() 
    { 
     distanceToPlayer = Vector2.Distance(transform.position,player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }
    
}
