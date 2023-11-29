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

    private void Awake()
    {
       weaponParent = GetComponentInChildren<WeaponParent>();
    }

    private void Update()
    {
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
