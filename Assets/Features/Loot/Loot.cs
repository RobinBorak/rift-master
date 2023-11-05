using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor4D.Common.Scripts.Enums;

public class Loot : MonoBehaviour
{
  public PlayerInventoryItem Item;

  // Start is called before the first frame update
  void Start()
  {
    if (Item.item.name == "Gold")
      gameObject.tag = "Gold";
  }

  // Update is called once per frame
  void Update()
  {

  }
}
