using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenText : MonoBehaviour
{
  [SerializeField] private GameObject screenText;
  [SerializeField] private TextMeshProUGUI screenTextHeader;
  [SerializeField] private TextMeshProUGUI screenTextBody;

  // Start is called before the first frame update
  void Start()
  {
    HideScreenText();

    CurrentRiftLogic.onRiftCompleteCallback += () =>
    {
      ShowScreenText("Rift complete!", "Going back to town...");
      StartCoroutine(UpdateGoingBackToTownText());
      Invoke("HideScreenText", 10.5f);
    };

    CurrentRiftLogic.onRiftFailedCallback += () =>
    {
      ShowScreenText("Rift failed!", "Going back to town...");
      StartCoroutine(UpdateGoingBackToTownText());
      Invoke("HideScreenText", 10.5f);
    };
  }

  public void ShowScreenText(string header, string body)
  {
    screenTextHeader.enabled = true;
    screenTextBody.enabled = true;
    screenTextHeader.text = header;
    screenTextBody.text = body;
  }

  public void HideScreenText()
  {
    screenTextHeader.enabled = false;
    screenTextBody.enabled = false;
  }

  public void UpdateBody(string body)
  {
    screenTextBody.text = body;
  }

  private IEnumerator UpdateGoingBackToTownText()
  {
    for (int i = 0; i < 10; i++)
    {
      UpdateBody("Going back to town in " + (10 - i) + " seconds...");
      yield return new WaitForSeconds(1f);
    }
  }
}
