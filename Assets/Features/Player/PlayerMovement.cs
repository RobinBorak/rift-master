using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

  private PlayerStats playerStats;

  private JoystickMovement joystickMovement;
  private float moveX;
  private float moveY;
  private Vector2 moveDirection;
  private Vector2 lastMoveDirection;

  private bool isDashing = false;
  private bool isDashingCooldown = false;

  private Animator animator;
  private Rigidbody2D rb;

  // Start is called before the first frame update
  void Start()
  {
    playerStats = gameObject.GetComponent<PlayerStats>();
    joystickMovement = FindObjectOfType<JoystickMovement>();
    animator = gameObject.GetComponent<Animator>();
    rb = gameObject.GetComponent<Rigidbody2D>();
  }

  void FixedUpdate()
  {
    Move();
    Animate();
  }

  private void Move()
  {

    moveX = joystickMovement.joystickVector.x;
    moveY = joystickMovement.joystickVector.y;

    moveDirection = new Vector2(moveX, moveY).normalized;
    if (moveX != 0 || moveY != 0)
    {
      lastMoveDirection = moveDirection;
    }
    if (!isDashing)
    {
      rb.velocity = new Vector2(moveDirection.x * playerStats.movementSpeed, moveDirection.y * playerStats.movementSpeed);
    }
  }

  private void Animate()
  {
    animator.SetFloat("AnimMoveX", moveX);
    animator.SetFloat("AnimMoveY", moveY);
    animator.SetFloat("AnimLastMoveX", lastMoveDirection.x);
    animator.SetFloat("AnimLastMoveY", lastMoveDirection.y);
    animator.SetBool("AnimIsMoving", rb.velocity.magnitude > 0);
  }

  public void Dash()
  {
    if (isDashingCooldown)
    {
      return;
    }
    Debug.Log("Dash");
    isDashing = true;
    rb.velocity = new Vector2(lastMoveDirection.x * 10, lastMoveDirection.y * 10);
    Invoke("StopDashing", 0.2f);
    Invoke("ResetDashingCooldown", 1f);
  }

  private void StopDashing()
  {
    isDashing = false;
  }

  private void ResetDashingCooldown()
  {
    isDashingCooldown = false;
  }
}
