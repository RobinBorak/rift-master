using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
  private static PlayerStats instance;
  private static string key = "PlayerStats";
  private SerializedPlayerStats serializedPlayerStats;

  public float maxHealth = 5f;
  public float movementSpeed = 5f;

  [Header("Combat")]
  public float attackSpeed = 1f;
  public float attackRange = 1f;

  [Header("Exp")]
  public int level = 1;
  public int currentExp = 0;


  void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject);
      SerializedPlayerStats serializedPlayerStats = (SerializedPlayerStats)Store.Load(key);

      if (serializedPlayerStats != null)
      {
        maxHealth = serializedPlayerStats.maxHealth;
        movementSpeed = serializedPlayerStats.movementSpeed;
        attackSpeed = serializedPlayerStats.attackSpeed;
        attackRange = serializedPlayerStats.attackRange;
        level = serializedPlayerStats.level;
        currentExp = serializedPlayerStats.currentExp;
      }
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void Save()
  {
    Store.Save(key, new SerializedPlayerStats(this));
  }

}
