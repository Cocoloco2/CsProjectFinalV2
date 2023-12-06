using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class Player : Entity
{ 
    public GameObject weapon;
    public GameObject heartbar;
    [SerializeField]
    private WeaponParent weaponParent;
    [SerializeField]
    private InputActionReference PointerPosition;
    private Vector2 pointerInput;

    private void Awake()
    {
        GameObject Weapon = Instantiate(weapon);
        weaponParent = Weapon.GetComponent<WeaponParent>();
        GameObject Heartbar = Instantiate(heartbar);
    }
   
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
