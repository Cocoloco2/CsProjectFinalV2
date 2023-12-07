using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class WeaponParent : MonoBehaviour
{
    public Vector2 PointerPosition { get; set; }

    private void Update()
    {
 
        //makes sure the object always spawn at the mouse position
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;
        //Debug.Log(direction);

        Vector2 scale = transform.localScale;
        
        //if scale is negative it will flip the object around
        if(direction.x < 0)
        {
            scale.y = -1;
        } else if(direction.x > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;
        
    }
}
