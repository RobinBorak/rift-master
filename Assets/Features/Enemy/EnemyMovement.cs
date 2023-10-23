using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

  private int maxDistanceToTargetToMove = 10;
  private float movementSpeed = 3f;

  EnemyCombat enemyCombat;

  private Rigidbody2D rb;
  private Transform target;
  private Animator anim;

  private float lastMoveX;
  private float lastMoveY;

  private float knockbackVelocity = 0f;
  private Transform knockbackFromTarget;

  // Start is called before the first frame update
  void Start()
  {
    rb = gameObject.GetComponent<Rigidbody2D>();
    target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    anim = gameObject.GetComponent<Animator>();

    Enemy enemyStats = gameObject.GetComponent<Enemy>();
    maxDistanceToTargetToMove = enemyStats.GetMaxDistanceToTargetToMove;
    movementSpeed = enemyStats.GetMovementSpeed;

    enemyCombat = gameObject.GetComponent<EnemyCombat>();

  }

  private void FixedUpdate()
  {
    Move(target);
    Animate();
  }

  private void Move(Transform target)
  {
    Vector2 velocity = Vector2.zero;
    if (
      Vector2.Distance(transform.position, target.position) < maxDistanceToTargetToMove
      && Vector2.Distance(transform.position, target.position) > enemyCombat.GetAttackRange - 0.5f
    )
    {
      lastMoveX = target.position.x - transform.position.x;
      lastMoveY = target.position.y - transform.position.y;

      Vector2 direction = (target.position - transform.position).normalized;
      velocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
    }
    else if (
      Vector2.Distance(transform.position, target.position) <= enemyCombat.GetAttackRange
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

  private void Animate()
  {
    anim.SetFloat("AnimMoveX", rb.velocity.x);
    anim.SetFloat("AnimMoveY", rb.velocity.y);
    anim.SetFloat("AnimLastMoveX", lastMoveX);
    anim.SetFloat("AnimLastMoveY", lastMoveY);
    anim.SetBool("AnimIsMoving", rb.velocity.magnitude > 0);
  }

}
