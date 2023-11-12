using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;

public class EnemyCombat : MonoBehaviour
{
  private EnemyStats enemyStats;
  private Enemy enemy;
  private Character4D character;
  private Animator anim;
  private bool isAttacking = false;

  private void Start()
  {
    enemyStats = gameObject.GetComponent<EnemyStats>();
    enemy = gameObject.GetComponent<Enemy>();
    character = gameObject.GetComponent<Character4D>();
    anim = gameObject.GetComponent<Animator>();
  }

  public void Attack()
  {
    if (!isAttacking)
    {
      character.AnimationManager.Slash(twoHanded: false);
      Swing();
      isAttacking = true;
    }
  }

  private void Swing()
  {
    Invoke("SwingDmg", enemyStats.attackSpeed / 2);
    Invoke("ResetIsAttacking", enemyStats.attackSpeed);
  }

  private void SwingDmg()
  {
    Transform attackPoint = transform; //.Find("PrimaryWeapon").transform;
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, enemyStats.attackRange, LayerMask.GetMask("Player"));
    foreach (Collider2D player in hitEnemies)
    {
      player.GetComponent<PlayerCombat>().TakeDamage(Random.Range(1, 3));
    }
  }

  private void ResetIsAttacking()
  {
    isAttacking = false;
  }

}
