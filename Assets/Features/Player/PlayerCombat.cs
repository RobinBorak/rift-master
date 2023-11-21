using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;

public class PlayerCombat : MonoBehaviour
{

  private Character4D character;
  private PlayerStats playerStats;
  private Player player;
  private PlayerHealth playerHealth;
  private Animator anim;
  private bool isAttacking = false;
  private float dodgeChance = 10f;

  private void Start()
  {
    playerStats = gameObject.GetComponent<PlayerStats>();
    player = gameObject.GetComponent<Player>();
    playerHealth = gameObject.GetComponent<PlayerHealth>();
    character = gameObject.GetComponent<Character4D>();
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
    Invoke("SwingDmg", playerStats.attackSpeed / 2);
    Invoke("ResetIsAttacking", playerStats.attackSpeed);
  }
  private void SwingDmg()
  {
    Transform attackPoint = transform; //.Find("PrimaryWeapon").transform;
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, playerStats.attackRange, LayerMask.GetMask("Enemy"));
    foreach (Collider2D enemy in hitEnemies)
    {
      int minDmg = playerStats.Damage - 1 < 0 ? 0 : playerStats.Damage - 1;
      int maxDmg = playerStats.Damage + 1;
      int dmg = Random.Range(minDmg, maxDmg);
      if (dmg > 0)
        enemy.GetComponent<Enemy>().TakeDamage(dmg, transform);
    }
  }

  private void ResetIsAttacking()
  {
    isAttacking = false;
  }

  public void TakeDamage(int damage)
  {
    if (Dodge())
      return;

    int _damage = ArmorModifier(damage);
    playerHealth.TakeDamage(_damage);
  }

  private int ArmorModifier(int damage)
  {
    int armor = playerStats.Armor;
    int _damage = damage * (1 - armor / 100);
    return _damage;
  }

  private bool Dodge()
  {
    if (Random.Range(0f, 100f) < dodgeChance)
    {
      Debug.Log("Dodge");
      return true;
    }
    return false;
  }

}
