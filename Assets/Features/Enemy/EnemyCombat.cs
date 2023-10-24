using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
  private EnemyStats enemyStats;
  private Animator anim;
  private bool isAttacking = false;

  private Transform attackPoint;

  private void Start()
  {
    enemyStats = gameObject.GetComponent<EnemyStats>();
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
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, enemyStats.attackRange, LayerMask.GetMask("Player"));
    foreach (Collider2D player in hitEnemies)
    {
      player.GetComponent<Player>().TakeDamage(1f);
    }

    Invoke("ResetIsAttacking", enemyStats.attackSpeed);
  }

  private void ResetIsAttacking()
  {
    isAttacking = false;
  }

}
