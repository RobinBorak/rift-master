using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
  private static PlayerInventory instance;
  private static string key = "PlayerInventory";
  private SerializedInventory serializedInventory;
  private int gold = 0;

  public delegate void OnGoldChangeDelegate(int amount);
  public static event OnGoldChangeDelegate onGoldChangeDelegate;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject);

      serializedInventory = (SerializedInventory)Store.Load(key);

      if (serializedInventory == null)
        serializedInventory = new SerializedInventory();

      gold = serializedInventory.gold;

      PlayerInventory.onGoldChangeDelegate += Save;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void Save(int amount)
  {
    Store.Save(key, new SerializedInventory(
      gold
    ));
  }


  public void AddGold(int amount)
  {
    gold += amount;
    onGoldChangeDelegate?.Invoke(amount);
  }

  //Getters and Setters
  public int Gold
  {
    get { return gold; }
  }

}
