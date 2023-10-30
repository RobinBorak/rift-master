using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using Assets.HeroEditor4D.Common.Scripts.Enums;

public class EnemyMovement : MonoBehaviour
{

  private EnemyStats enemyStats;
  private EnemyCombat enemyCombat;

  private Rigidbody2D rb;
  private Transform target;
  private Animator anim;

  private float lastMoveX;
  private float lastMoveY;
  private bool _moving = false;

  private float knockbackVelocity = 0f;
  private Transform knockbackFromTarget;

  private Character4D Character;

  // Start is called before the first frame update
  void Start()
  {
    enemyStats = gameObject.GetComponent<EnemyStats>();
    enemyCombat = gameObject.GetComponent<EnemyCombat>();

    rb = gameObject.GetComponent<Rigidbody2D>();
    target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    anim = gameObject.GetComponent<Animator>();
    Character = gameObject.GetComponent<Character4D>();

    Character.AnimationManager.SetState(CharacterState.Idle);
    Character.SetDirection(Vector2.down);

  }

  private void FixedUpdate()
  {
    Move(target);
    SetDirection();
  }


  private void Move(Transform target)
  {
    Vector2 velocity = Vector2.zero;
    if (
      Vector2.Distance(transform.position, target.position) < enemyStats.maxDistanceToTargetToMove
      && Vector2.Distance(transform.position, target.position) > enemyStats.attackRange - 0.5f
    )
    {
      lastMoveX = target.position.x - transform.position.x;
      lastMoveY = target.position.y - transform.position.y;

      velocity = new Vector2(lastMoveX * enemyStats.movementSpeed, lastMoveY * enemyStats.movementSpeed).normalized;
    }
    else if (
      Vector2.Distance(transform.position, target.position) <= enemyStats.attackRange
    )
    {
      enemyCombat.Attack();
    }

    if (knockbackVelocity > 0f)
    {
      velocity = GetKnockbackVelocity();
    }

    rb.velocity = velocity;
    EmptyKnockback();

    if (velocity == Vector2.zero)
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

  public void Knockback(Transform position, float force)
  {
    knockbackVelocity = force;
    knockbackFromTarget = position;
  }

  private Vector2 GetKnockbackVelocity()
  {
    return new Vector2(
      (transform.position.x - knockbackFromTarget.position.x) * knockbackVelocity,
      (transform.position.y - knockbackFromTarget.position.y) * knockbackVelocity
    );
  }

  private void EmptyKnockback()
  {
    knockbackVelocity = 0f;
    knockbackFromTarget = null;
  }

  private void SetDirection()
  {
    float _moveX = Mathf.Round(lastMoveX);
    float _moveY = Mathf.Round(lastMoveY);
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

}
