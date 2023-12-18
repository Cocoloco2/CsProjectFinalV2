using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed;

    [SerializeField]
    private float collisionOffset = 0.05f;

    [SerializeField]
    private ContactFilter2D movementFilter;

    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private void Start()
    {
    //uses the get method from the "Player" script to store it inside the moveSpeed variable
    moveSpeed = GetComponent<Player>().getMoveSpeed();
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
        
    }

    private void FixedUpdate()
    {
        //if the player is not already getting an input
        if (movementInput != Vector2.zero)
        {
            //check if there is no objects blocking
            bool success = TryMove(movementInput);

            if (!success)
            {
                //check if there is no obstacle blocking on the x axis
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    //check is there is no obstacle on the y axis
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
            animator.SetBool("isMoving", success);
        }
        else 
        {
            animator.SetBool("isMoving", false);
        }
    }

    private bool TryMove(Vector2 direction) {

        //stores the amount of collisions inside the count variable  
        int count = rb.Cast(
            movementInput,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset
            );
        // there is no collisons
        if (count == 0)
        {
            //move the RigidBody of the GameObject, making it move in the direction input
            rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
        
            return true;
         
        } else {
            
            return false;
        }
    }
    //method used by the input system to get the input from the keyboard
    void OnMove (InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    
    }
    
}
