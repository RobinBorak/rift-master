using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

  private float movementSpeed = 5f;
  private PlayerStats playerStats;

  private JoystickMovement joystickMovement;
  private float moveX;
  private float moveY;
  private Vector2 moveDirection;
  private Vector2 lastMoveDirection;

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
    rb.velocity = new Vector2(moveDirection.x * playerStats.movementSpeed, moveDirection.y * playerStats.movementSpeed);
  }

  private void Animate()
  {
    animator.SetFloat("AnimMoveX", moveX);
    animator.SetFloat("AnimMoveY", moveY);
    animator.SetFloat("AnimLastMoveX", lastMoveDirection.x);
    animator.SetFloat("AnimLastMoveY", lastMoveDirection.y);
    animator.SetBool("AnimIsMoving", rb.velocity.magnitude > 0);
  }
}
