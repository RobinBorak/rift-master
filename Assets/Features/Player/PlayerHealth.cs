using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
  public float currentHealth;
  private PlayerStats playerStats;
  private Player player;

  public delegate void PlayerHealthLossDelegate();
  public static event PlayerHealthLossDelegate playerHealthLossDelegate;
  // Start is called before the first frame update
  void Start()
  {
    playerStats = gameObject.GetComponent<PlayerStats>();
    player = gameObject.GetComponent<Player>();
    Reset();
    Player.playerResetDelegate += Reset;
  }

  private void Reset()
  {
    HealToFull();
  }

  public void TakeDamage(float damage)
  {
    currentHealth -= damage;
    if (currentHealth <= 0)
    {
      player.Die();
    }
    playerHealthLossDelegate?.Invoke();
  }

  public void Heal(float amount)
  {
    currentHealth += amount;
    if (currentHealth > playerStats.MaxHealth)
    {
      currentHealth = playerStats.MaxHealth;
    }
    playerHealthLossDelegate?.Invoke();
  }

  public void HealToFull()
  {
    currentHealth = playerStats.MaxHealth;
    playerHealthLossDelegate?.Invoke();
  }

}
