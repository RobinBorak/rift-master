using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayCanvas : MonoBehaviour
{
  private static OverlayCanvas instance;

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
}
