using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
  public PlayerInventoryItem Item;

  // Start is called before the first frame update
  void Start()
  {

    if (Item.item.name == "Gold")
      gameObject.tag = "Gold";

    if (Item.item.name == "Helmet")
      transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
