using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
  [SerializeField] private Image healthBar;
  [SerializeField] private TextMeshProUGUI healthText;
  private Player player;
  private PlayerStats playerStats;
  private PlayerHealth playerHealth;


  void Start()
  {
    player = FindObjectOfType<Player>();
    playerStats = player.GetComponent<PlayerStats>();
    playerHealth = player.GetComponent<PlayerHealth>();
    PlayerHealth.playerHealthLossDelegate += UpdateHealthBar;
    Player.playerResetDelegate += UpdateHealthBar;
    PlayerTalents.onTalentsChangedCallback += UpdateHealthBar;

    UpdateHealthBar();
  }

  private void UpdateHealthBar()
  {
    healthBar.fillAmount = playerHealth.currentHealth / playerStats.MaxHealth;
    healthText.text = playerHealth.currentHealth.ToString() + " / " + playerStats.MaxHealth.ToString();
  }

}
