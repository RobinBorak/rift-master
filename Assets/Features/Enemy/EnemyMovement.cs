using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

  private EnemyStats enemyStats;
  private EnemyCombat enemyCombat;

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
    enemyStats = gameObject.GetComponent<EnemyStats>();
    enemyCombat = gameObject.GetComponent<EnemyCombat>();

    rb = gameObject.GetComponent<Rigidbody2D>();
    target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    anim = gameObject.GetComponent<Animator>();
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
      Vector2.Distance(transform.position, target.position) < enemyStats.maxDistanceToTargetToMove
      && Vector2.Distance(transform.position, target.position) > enemyStats.attackRange - 0.5f
    )
    {
      lastMoveX = target.position.x - transform.position.x;
      lastMoveY = target.position.y - transform.position.y;

      Vector2 direction = (target.position - transform.position).normalized;
      velocity = new Vector2(direction.x * enemyStats.movementSpeed, direction.y * enemyStats.movementSpeed);
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
