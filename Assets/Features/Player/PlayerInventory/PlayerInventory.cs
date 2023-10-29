using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
  private static PlayerInventory instance;
  private int gold = 0;

  public delegate void OnGoldChangeDelegate(int gold);
  public static event OnGoldChangeDelegate onGoldChangeDelegate;

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
