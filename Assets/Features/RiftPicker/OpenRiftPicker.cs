using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRiftPicker : MonoBehaviour
{
  [SerializeField] private GameObject riftPicker;
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      ShowRiftPicker();
    }
  }
  // Show Rift levels
  private void ShowRiftPicker()
  {
    // Show Rift levels
    riftPicker.SetActive(true);
  }

  public void HideRiftPicker()
  {
    // Hide Rift levels
    riftPicker.SetActive(false);
  }
}
