using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  private EnemyStats enemyStats;

  [SerializeField] private int currentHealth;
  private Rigidbody2D rb;

  public delegate void OnDeathDelegate();
  public OnDeathDelegate onDeathDelegate;

  public delegate void OnAnyEnemyDeathDelegate();
  public static OnAnyEnemyDeathDelegate onAnyEnemyDeathDelegate;

  // Start is called before the first frame update
  IEnumerator Start()
  {
    enemyStats = gameObject.GetComponent<EnemyStats>();
    rb = gameObject.GetComponent<Rigidbody2D>();

    while (!enemyStats.isDoneLoading)
    {
      yield return null;
    }

    currentHealth = enemyStats.maxHealth;

  }

  // Update is called once per frame
  void Update()
  {

  }
  public void TakeDamage(int damage)
  {
    currentHealth -= damage;
    if (currentHealth <= 0)
    {
      Die();
    }
  }
  public void TakeDamage(int damage, Transform fromTarget)
  {
    TakeDamage(damage);
    gameObject.GetComponent<EnemyMovement>().Knockback(fromTarget, 50f);
  }
  private void Die()
  {
    FindObjectOfType<Player>().GetComponent<Player>()?.GainExp(enemyStats.exp);
    FindObjectOfType<CurrentRiftLogic>()?.IncreaseSmallProgress();
    onDeathDelegate?.Invoke();
    onAnyEnemyDeathDelegate?.Invoke();
    Destroy(gameObject);
  }
}
