using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;

public class PlayerCombat : MonoBehaviour
{

  private Character4D Character;
  private PlayerStats playerStats;
  private Animator anim;
  private bool isAttacking = false;

  private Transform attackPoint;

  private void Start()
  {
    playerStats = gameObject.GetComponent<PlayerStats>();
    Character = gameObject.GetComponent<Character4D>();
    //attackPoint = transform.Find("AttackPoint").transform;
    attackPoint = transform;
  }

  public void Attack()
  {
    if (!isAttacking)
    {
      Character.AnimationManager.Slash(twoHanded: false);
      Swing();
      isAttacking = true;
    }
  }

  private void Swing()
  {
    Invoke("SwingDmg", playerStats.attackSpeed / 2);
    Invoke("ResetIsAttacking", playerStats.attackSpeed);
  }
  private void SwingDmg()
  {
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, playerStats.attackRange, LayerMask.GetMask("Enemy"));
    foreach (Collider2D enemy in hitEnemies)
    {
      enemy.GetComponent<Enemy>().TakeDamage(1f, transform);
    }
  }

  private void ResetIsAttacking()
  {
    isAttacking = false;
  }
}
