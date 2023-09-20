using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] ContactFilter2D movementFilter;
    [SerializeField] float collisionOffset = 0.05f;
    [SerializeField] SwordAttack swordAttack;   
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    bool canMove = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();   
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);
                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }
                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }

            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }

    }
    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                  direction,
                  movementFilter,
                  castCollisions,
                  moveSpeed * Time.fixedDeltaTime + collisionOffset
              );
            if (count == 0)
            {
                rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * direction);
                return true;
            }
            else
            {
                return false;
            }
        } else
        {
            return false;
        }

    }
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();   

    }

    void OnFire()
    {
        animator.SetTrigger("Attack");
    }

    public void LockMovement()
    {
        canMove = false;    
    }


    public void SwordAttack()
    {
        LockMovement();
        if(spriteRenderer.flipX== true)
        {
            swordAttack.AttackLeft();
        } else
        {
            swordAttack.AttackRight();

        }
    }
    public void StopAttack()
    {
        UnLockMovement();
        swordAttack.StopAttack();
    }
    public void UnLockMovement()
    {
        canMove = true;

    }


}
