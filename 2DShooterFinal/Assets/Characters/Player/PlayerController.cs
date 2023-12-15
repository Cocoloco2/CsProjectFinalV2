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

    // Start is called before the first frame update
    private void Start()
    {
    moveSpeed = GetComponent<Player>().getMoveSpeed();
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
        
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
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

            int count = rb.Cast(
                movementInput,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
                );
            if (count == 0)
            {
                rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
            Debug.Log(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
                return true;
         
        } else {
            //Debug.Log(count);
            return false;
        }
    }
    
    void OnMove (InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    
    }
    
}
