using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
  private static PlayerStats instance;
  private static string key = "PlayerStats";
  private SerializedPlayerStats serializedPlayerStats;
  private PlayerTalents playerTalents;
  private PlayerEquipment playerEquipment;

  private int maxHealth = 20;
  private float movementSpeed = 2.5f;
  private int armor = 0;
  private int damage = 1;

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
      playerEquipment = FindObjectOfType<PlayerEquipment>();
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
  public int MaxHealth
  {
    get { return maxHealth + (playerTalents.GetTalentPoints(1) * 5); }
  }

  public float MovementSpeed
  {
    get { return movementSpeed + (float)playerTalents.GetTalentPoints(2) * 0.03f; }
  }

  public int Armor
  {
    get { return armor + playerEquipment.GetArmor(); }
  }

  public int Damage
  {
    get { return damage + playerEquipment.GetDamage(); }
  }
}
