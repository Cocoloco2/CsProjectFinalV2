using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : WeaponParent 
    
{
    
    private Camera cam;
   

    Vector2 mousePos;

    private void Awake()
    {
        cam = Camera.main;   
    }

    // Update is called once per frame
    void Update()
    {
     
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        float z = transform.position.z;


        Vector2 dir = mousePos - position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        z = angle;
        
    }
    
}
