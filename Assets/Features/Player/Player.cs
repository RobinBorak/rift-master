using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] private float maxHealth = 5f;
  private float currentHealth;
  // Start is called before the first frame update
  void Start()
  {
    currentHealth = maxHealth;
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
