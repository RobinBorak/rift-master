using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using Assets.HeroEditor4D.Common.Scripts.Enums;

public class PlayerMovement : MonoBehaviour
{

  private PlayerStats playerStats;

  private JoystickMovement joystickMovement;
  private float moveX;
  private float moveY;
  private Vector2 moveDirection;
  private Vector2 lastMoveDirection;
  private bool _moving = false;

  private bool isDashing = false;
  private bool isDashingCooldown = false;

  private Animator animator;
  private Rigidbody2D rb;

  public Character4D Character;

  // Start is called before the first frame update
  void Start()
  {
    playerStats = gameObject.GetComponent<PlayerStats>();
    joystickMovement = FindObjectOfType<JoystickMovement>();
    animator = gameObject.GetComponent<Animator>();
    rb = gameObject.GetComponent<Rigidbody2D>();


    Character.AnimationManager.SetState(CharacterState.Idle);
    Character.SetDirection(Vector2.down);
  }

  void FixedUpdate()
  {
    Move();
    SetDirection();
  }

  private void Move()
  {
    moveX = joystickMovement.joystickVector.x;
    moveY = joystickMovement.joystickVector.y;

    moveDirection = new Vector2(moveX, moveY).normalized;
    if (moveDirection != Vector2.zero)
    {
      lastMoveDirection = moveDirection;
    }
    if (!isDashing)
    {
      rb.velocity = new Vector2(moveDirection.x * playerStats.movementSpeed, moveDirection.y * playerStats.movementSpeed);
    }

    if (moveDirection == Vector2.zero)
    {
      if (_moving)
      {
        _moving = false;
        Character.AnimationManager.SetState(CharacterState.Idle);
      }
    }
    else
    {
      _moving = true;
      Character.AnimationManager.SetState(CharacterState.Run);
    }
  }

  private void SetDirection()
  {
    float _moveX = Mathf.Round(moveX);
    float _moveY = Mathf.Round(moveY);
    Vector2 direction = Vector2.zero;

    if (_moveX == -1 && _moveY == 0)
    {
      direction += Vector2.left;
    }
    else if (_moveX == 1 && _moveY == 0)
    {
      direction += Vector2.right;
    }
    else if (_moveX == 0 && _moveY == 1)
    {
      direction += Vector2.up;
    }
    else if (_moveX == 0 && _moveY == -1)
    {
      direction += Vector2.down;
    }
    else return;

    Character.SetDirection(direction);
  }

  public void Dash()
  {
    if (isDashingCooldown)
      return;
    isDashing = true;
    isDashingCooldown = true;
    rb.velocity = new Vector2(lastMoveDirection.x * 10, lastMoveDirection.y * 10);
    Invoke("StopDashing", 0.2f);
    Invoke("ResetDashingCooldown", 1.5f);
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
