using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

  public delegate void ChestDelegate();
  public event ChestDelegate onChestOpenDelegate;

  //On trigger, destroy chest and spawn loot
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      onChestOpenDelegate?.Invoke();
      Destroy(gameObject);
    }
  }

}
