using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayGold : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI goldText;
  // Start is called before the first frame update
  void Start()
  {
    UpdateGoldText(0);
    PlayerInventory.onGoldChangeDelegate += UpdateGoldText;
  }

  private void UpdateGoldText(int amount)
  {
    int gold = FindObjectOfType<PlayerInventory>().Gold;
    goldText.text = gold.ToString();
  }
} 
