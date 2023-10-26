using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTalentCanvas : MonoBehaviour
{
  public GameObject talentCanvas;

  public void HideTalentCanvas()
  {
    talentCanvas.SetActive(false);
  }

  public void ShowTalentCanvas()
  {
    talentCanvas.SetActive(true);
  }

}
