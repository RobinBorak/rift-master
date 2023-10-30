using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
  private static PlayerStats instance;
  private static string key = "PlayerStats";
  private SerializedPlayerStats serializedPlayerStats;
  private PlayerTalents playerTalents;

  private float maxHealth = 5f;
  public float movementSpeed = 2.5f;

  [Header("Combat")]
  public float attackSpeed = 1f;
  public float attackRange = 1f;

  [Header("Exp")]
  public int level;
  public int currentExp;


  void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject);
      playerTalents = FindObjectOfType<PlayerTalents>();
      serializedPlayerStats = (SerializedPlayerStats)Store.Load(key);

      if (serializedPlayerStats == null)
        serializedPlayerStats = new SerializedPlayerStats();

      level = serializedPlayerStats.level;
      currentExp = serializedPlayerStats.currentExp;

    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void Save()
  {
    Store.Save(key, new SerializedPlayerStats(
      level,
      currentExp
    ));
  }

  //Getters and Setters
  public float MaxHealth
  {
    get { return maxHealth + (float)playerTalents.GetTalentPoints(1); }
  }
}
