using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
  
    private GameObject player;
    private float distanceToPlayer;

    [SerializeField]
    private bool debugDistanceToPlayer = false;
    private void Update()
    {
        trackPlayer();
        if (health <= 0) { 
            Destroy(gameObject);
            }
       
    }
    private void trackPlayer() 
    {
        player = GameObject.FindWithTag("Player");
        //distanceToPlayer = Vector2.Distance(transform.position,player.transform.position);
      
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
        }
    }

}
