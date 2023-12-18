/*
This code is inspired by BMo
march 21. 2022
https://www.youtube.com/watch?v=5NViMw-ALAo&t=1s
*/

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

    List<heart_script> hearts = new List<heart_script>(); //list to contain the hearts
    
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

        DrawHearts();
    }

    private void Update()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");//finds the object with 'Player' tag
        player = playerObj.GetComponent<Player>();//access to variables in 'Player' component
   
    }

    public void DrawHearts()
    {
        ClearHearts();

        //Determine how many hearts to make total 
        // based off the max health
       
        float maxHealthRemainder = player.getMaxHealth() % 2; //calculates remainder. either 0 if even or 1 if uneven.
        //Debug.Log("maxHealth is: "+player.maxHealth);
        int heartsToMake = (int)((player.getMaxHealth() / 2) + maxHealthRemainder); //calculates hearts to draw
        for(int i = 0; i < heartsToMake; i++) 
        {
            CreateEmptyHeart(); //creates empty hearts until the above value has been met
        }

        for(int i =0; i<hearts.Count; i++)
        {
            //clamps the values to return 0, 1 or 2 corresponding to the heartstatus enum
            int heartStatusRemainder = (int)Mathf.Clamp(player.getHealth() - (i * 2), 0, 2); 
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder); //fills the empty hearts according to the values returned above
        }
    }


    public void CreateEmptyHeart()
    {
        
        GameObject newHeart = Instantiate(heartPrefab); //instantiate heartprefab
        newHeart.transform.SetParent(transform, false);

        heart_script heartComponent = newHeart.GetComponent<heart_script>(); 
        heartComponent.SetHeartImage(HeartStatus.Empty); //sets the heartstatus to empty
        hearts.Add(heartComponent); //add the empty heart to list
        //newHeart.transform.localScale = new Vector2(2, 2);
        
    }

    public void ClearHearts()//clear all hearts to make sure the game always starts from a fresh
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<heart_script> ();
    }
}
