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
    SetAnimationSpeed();
  }

  public void Attack()
  {
    if (!isAttacking)
    {
      isAttacking = true;
      anim.SetTrigger("Attack");
      Swing();
    }
  }

  private void Swing()
  {
    Invoke("SwingDmg", enemyStats.attackSpeed / 2);
    Invoke("ResetIsAttacking", enemyStats.attackSpeed);
  }

  private void SwingDmg()
  {
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, enemyStats.attackRange, LayerMask.GetMask("Player"));
    foreach (Collider2D player in hitEnemies)
    {
      player.GetComponent<Player>().TakeDamage(1f);
    }
  }

  private void ResetIsAttacking()
  {
    isAttacking = false;
  }

  private void SetAnimationSpeed()
  {
    Animator anim = gameObject.GetComponent<Animator>();
    anim.speed = enemyStats.attackSpeed / 2;
  }


}
