using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
  [SerializeField] private Image healthBar;
  [SerializeField] private Player player;
  private PlayerStats playerStats;


  void Start()
  {
    playerStats = player.GetComponent<PlayerStats>();
    Player.playerHealthLossDelegate += UpdateHealthBar;
    Player.playerResetDelegate += Reset;

    Reset();
  }

  private void UpdateHealthBar(float damage)
  {
    Debug.Log("Updating health bar " + player.currentHealth + " / " + playerStats.maxHealth + " = " + player.currentHealth / playerStats.maxHealth);
    healthBar.fillAmount = player.currentHealth / playerStats.maxHealth;
  }

  private void Reset()
  {
    healthBar.fillAmount = 1;
  }

}
