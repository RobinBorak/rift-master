using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PopulateAvailableRifts : MonoBehaviour
{
  private PlayerRiftsStats playerRiftsStats;
  private List<RiftStats> riftsStats = new List<RiftStats>();
  [SerializeField] private GameObject buttonPrefab;
  void OnEnable()
  {
    riftsStats = PlayerRiftsStats.Get();
    Debug.Log("Rifts stats: " + riftsStats.Count);

    CreateRiftButtons();
  }

  private void CreateRiftButtons()
  {
    foreach (Transform child in transform)
    {
      Destroy(child.gameObject);
    }

    for (int i = riftsStats.Count; i >= 0; i--)
    {
      int rift = i+1;
      GameObject button = Instantiate(buttonPrefab) as GameObject;
      button.transform.SetParent(transform, false);
      button.GetComponent<SelectRift>().SetRift(rift);

      //Set button text
      button.GetComponentInChildren<TextMeshProUGUI>().text = "Rift " + (rift);
    }
  }

}
