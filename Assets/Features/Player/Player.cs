using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private PlayerStats playerStats;
  private float currentHealth;
  // Start is called before the first frame update
  void Start()
  {
    playerStats = gameObject.GetComponent<PlayerStats>();
    currentHealth = playerStats.maxHealth;
  }

  public void TakeDamage(float damage)
  {
    currentHealth -= damage;
    if (currentHealth <= 0)
    {
      Die();
    }
  }

  private void Die()
  {
  }

}
