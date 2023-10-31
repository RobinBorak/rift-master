using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
  [SerializeField] private int value = 5;

  // Getters and Setters
  public int Value
  {
    get { return value; }
  }
}
