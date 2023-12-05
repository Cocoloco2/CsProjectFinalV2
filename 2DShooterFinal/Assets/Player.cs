using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    public WeaponParent weaponParent;
    [SerializeField]
    private InputActionReference PointerPosition;
    private Vector2 pointerInput;

    //denne linje skal kun bruges hvis weapon er et child til player
    /*
    private void Awake()
    {
       weaponParent = GetComponent<WeaponParent>();
    }
    */
    private void Update()
    {
        weaponParent.transform.position = transform.position;
        pointerInput = getPointerInput();
        weaponParent.PointerPosition = pointerInput;
       
    }
    private Vector2 getPointerInput() 
    {
        Vector3 mousePos = PointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
