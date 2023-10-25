using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  private EnemyStats enemyStats;

  private float currentHealth;
  private Rigidbody2D rb;
  // Start is called before the first frame update
  void Start()
  {
    enemyStats = gameObject.GetComponent<EnemyStats>();
    currentHealth = enemyStats.maxHealth;
    rb = gameObject.GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {

  }
  public void TakeDamage(float damage)
  {
    currentHealth -= damage;
    if (currentHealth <= 0)
    {
      Die();
    }
  }
  public void TakeDamage(float damage, Transform fromTarget)
  {
    TakeDamage(damage);
    gameObject.GetComponent<EnemyMovement>().Knockback(fromTarget, 50f);
  }
  private void Die()
  {
    FindObjectOfType<Player>().GetComponent<Player>()?.GainExp(enemyStats.exp);
    FindObjectOfType<CurrentRiftLogic>()?.IncreaseSmallProgress();
    Destroy(gameObject);
  }
}
