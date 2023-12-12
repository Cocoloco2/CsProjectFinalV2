using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class Heartbar : MonoBehaviour
{
    public GameObject heartPrefab;
    public GameObject playerObj;
    public Player player;
    private int health;
    private int maxHealth;


    
    //public int health = 2;
    //public int maxhealth = 4;
    List<heart_script> hearts = new List<heart_script>();
    
  
    
    private void OnEnable()
    {
        Player.OnDamaged += DrawHearts;
    }

    private void OnDisable()
    {
        Player.OnDamaged -= DrawHearts;
    }
    
    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();
        health = player.getHealth();
        maxHealth = player.getMaxHealth();
        DrawHearts();
    }

    private void Update()
    {
      
        
    }

    public void DrawHearts()
    {
        ClearHearts();

        //Determine how many hearts to make total 
        // based off the max health
       
        float maxHealthRemainder = maxHealth % 2;
        //Debug.Log("maxHealth is: "+player.maxHealth);
        int heartsToMake = (int)((maxHealth / 2) + maxHealthRemainder);
        for(int i = 0; i < heartsToMake; i++) 
        {
            CreateEmptyHeart();
        }

        for(int i =0; i<hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(health - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }


    public void CreateEmptyHeart()
    {
        
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform, false);

        heart_script heartComponent = newHeart.GetComponent<heart_script>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
        //newHeart.transform.localScale = new Vector2(2, 2);
        
    }

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<heart_script> ();
    }



}
