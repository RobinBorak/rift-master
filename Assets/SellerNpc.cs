using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerNpc : MonoBehaviour
{

  [SerializeField] private Canvas canvas;

  // On 2d trigger
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      canvas.gameObject.SetActive(true);
    }
  }
}
