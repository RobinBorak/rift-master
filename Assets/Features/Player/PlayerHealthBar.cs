using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
  [SerializeField] private Image healthBar;
  [SerializeField] private Player player;
  [SerializeField] private TextMeshProUGUI healthText;
  private PlayerStats playerStats;


  void Start()
  {
    playerStats = player.GetComponent<PlayerStats>();
    Player.playerHealthLossDelegate += UpdateHealthBar;
    Player.playerResetDelegate += UpdateHealthBar;
    PlayerTalents.onTalentsChangedCallback += UpdateHealthBar;

    UpdateHealthBar();
  }

  private void UpdateHealthBar()
  {
    healthBar.fillAmount = player.currentHealth / playerStats.MaxHealth;
    healthText.text = player.currentHealth.ToString() + " / " + playerStats.MaxHealth.ToString();
  }

}
