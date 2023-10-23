using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
  private Animator anim;
  private float attackSpeed = 1.5f;
  private float attackRange = 1.5f;
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
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Player"));
    foreach (Collider2D player in hitEnemies)
    {
      player.GetComponent<Player>().TakeDamage(1f);
    }

    Invoke("ResetIsAttacking", attackSpeed);
  }

  private void ResetIsAttacking()
  {
    isAttacking = false;
  }


  // Getters and Setters
  public float GetAttackRange
  {
    get { return attackRange; }
  }


}
