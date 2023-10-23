using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

  private PlayerStats playerStats;
  private Animator anim;
  private bool isAttacking = false;

  private Transform attackPoint;

  private void Start()
  {
    playerStats = gameObject.GetComponent<PlayerStats>();
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
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, playerStats.attackRange, LayerMask.GetMask("Enemy"));
    foreach (Collider2D enemy in hitEnemies)
    {
      enemy.GetComponent<Enemy>().TakeDamage(1f, transform);
    }

    Invoke("ResetIsAttacking", playerStats.attackSpeed);
  }

  private void ResetIsAttacking()
  {
    isAttacking = false;
  }
}
