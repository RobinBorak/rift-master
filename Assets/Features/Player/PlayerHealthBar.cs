using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
  private static PlayerHealthBar instance;

  [SerializeField] private Image healthBar;
  [SerializeField] private Player player;
  private PlayerStats playerStats;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  void Start()
  {
    playerStats = player.GetComponent<PlayerStats>();
    Player.playerHealthLossDelegate += UpdateHealthBar;
    Player.playerResetDelegate += Reset;
    CurrentRiftLogic.currentRiftLogicRiftCompleteDelegate += OnCurrentRiftLogicRiftComplete;

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

  private void OnDestroy()
  {
    Player.playerHealthLossDelegate -= UpdateHealthBar;
    Player.playerResetDelegate -= Reset;
    CurrentRiftLogic.currentRiftLogicRiftCompleteDelegate -= OnCurrentRiftLogicRiftComplete;
  }

  private void OnCurrentRiftLogicRiftComplete()
  {
    Destroy(gameObject);
  }

}
