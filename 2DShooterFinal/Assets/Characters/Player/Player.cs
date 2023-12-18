using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class Player : Entity
{
    [SerializeField]
    private GameObject weapon;

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
        camera1 = Camera.main;

    }
   
    private void Update()
    {
        //sets the main camera to be equal to the player transform and sets the depth to be -10 away from the player
        camera1.transform.position = this.transform.position+new Vector3(0,0,-10);
        //sets the position of the pistol to relate to the players
        weaponParent.transform.position = transform.position;
        
        pointerInput = getPointerInput();
        //set the PointerPosition inside weaponParent to pointerInput moving the guns z axis in accordance with the players relation to the mouse
        weaponParent.PointerPosition = pointerInput;
        
        if (health == 0)
        {
            //load a new scene 
            SceneManager.LoadSceneAsync(0);
        }
    }
    private Vector2 getPointerInput() 
    {
        //store the mouse position by reading the InputActionReference from the input system
        //note that because it is a Vector2 being read the Vector3 variable we are storing the value in will have 0 in the z axis by default
        Vector3 mousePos = PointerPosition.action.ReadValue<Vector2>();
        //set the z axis to be equal the the cameras nearClipPlane
        mousePos.z = Camera.main.nearClipPlane;
        
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
