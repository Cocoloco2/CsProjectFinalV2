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

    //has not been implemented in this iteration
    /*
    [SerializeField]
    private bool debugDistanceToPlayer = false;
    */

    private void Update()
    {
        trackPlayer();
        if (health <= 0) { 
            Destroy(gameObject);
            }
       
    }
    private void trackPlayer() 
    {
        //look for at GameObject with the "Player" tag and store it inside the player variable
        player = GameObject.FindWithTag("Player");
       
        //sets the objects position memicking  smooth movemet towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    //check for collision with the "Player" GameObject
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //if there is collision with a player, the player takes damge equal to the amount specified
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
        }
    }

}
