using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
  public LootItem Item;
  // Start is called before the first frame update
  void Start()
  {
    if (Item.Name == "Gold")
      gameObject.tag = "Gold";
  }

  // Update is called once per frame
  void Update()
  {

  }
}
