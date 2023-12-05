using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class WeaponParent : MonoBehaviour
{
    public Vector2 PointerPosition { get; set; }
    public int Index {get; set;}
    //[SerializeField] private InputActionReference PointerPosition;

    
    private void Update()
    {
        //makes sure the object always spawn at the mouse position
        //Vector3 test = PointerPosition.action.ReadValue<Vector2>();
        //Debug.Log(test);
        
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

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
    /*
    private Vector2 getPointerInput()
    {
        Vector3 mousePos = PointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    */
}
