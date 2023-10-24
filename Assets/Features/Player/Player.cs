using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private PlayerStats playerStats;
  private float currentHealth;
  private CurrentRiftLogic currentRiftLogic;
  // Start is called before the first frame update
  void Start()
  {
    currentRiftLogic = FindObjectOfType<CurrentRiftLogic>();
    Reset();
  }

  private void Reset()
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
    Debug.Log("Player died");
    //Find RespawnPosition by name
    Transform respawnPosition = GameObject.Find("RespawnPosition").transform;
    transform.position = respawnPosition.position;
    currentRiftLogic.DecreaseSmallProgress();
    Reset();
  }

}
