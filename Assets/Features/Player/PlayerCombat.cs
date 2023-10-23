using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
  private Animator anim;
  private float attackSpeed = 1f;
  private float attackRange = 2f;
  private bool isAttacking = false;

  private Transform attackPoint;

  private void Start()
  {
    anim = gameObject.GetComponent<Animator>();
    attackPoint = transform.Find("AttackPoint").transform;
  }

  public void Attack()
  {
    if (!isAttacking)
    {
      anim.SetTrigger("Attack");
      Swing();
      isAttacking = true;
    }
  }

  private void Swing()
  {
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Enemy"));
    foreach (Collider2D enemy in hitEnemies)
    {
      enemy.GetComponent<Enemy>().TakeDamage(1f, transform);
    }

    Invoke("ResetIsAttacking", attackSpeed);
  }

  private void ResetIsAttacking()
  {
    isAttacking = false;
  }
}
