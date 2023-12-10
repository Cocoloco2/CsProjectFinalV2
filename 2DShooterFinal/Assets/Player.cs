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
    Camera camera1;

    private void Awake()
    {
        GameObject Weapon = Instantiate(weapon);
        weaponParent = Weapon.GetComponent<WeaponParent>();
       // GameObject Heartbar = Instantiate(heartbar);
        camera1 = Camera.main;

    }
   
    private void Update()
    {
        //sets the main camera to be equal to the player transform and sets the depth to be -10 away from the player
        camera1.transform.position = this.transform.position+new Vector3(0,0,-10);
        weaponParent.transform.position = transform.position;
        pointerInput = getPointerInput();
        weaponParent.PointerPosition = pointerInput;
        Debug.Log(health);
    }
    private Vector2 getPointerInput() 
    {
        Vector3 mousePos = PointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
