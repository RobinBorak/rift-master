using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DashButton : MonoBehaviour
{
  private PlayerMovement playerMovement;
  private float dashingCooldown;
  [SerializeField] private Image cooldownImage;
  // Start is called before the first frame update
  void Start()
  {
    playerMovement = FindObjectOfType<PlayerMovement>();
    dashingCooldown = playerMovement.dashingCooldown;
    cooldownImage.fillAmount = 0;
    InitDashButton();
  }

  private void InitDashButton()
  {
    //On attackButton pointer down
    EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
    EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
    pointerDownEntry.eventID = EventTriggerType.PointerDown;
    pointerDownEntry.callback.AddListener((data) => { Dash(); });
    trigger.triggers.Add(pointerDownEntry);
  }

  private void Dash()
  {
    bool isDashing = playerMovement.Dash();
    if (isDashing)
      StartCoroutine(DashCooldown());
  }

  private IEnumerator DashCooldown()
  {
    cooldownImage.fillAmount = 1;
    float time = 0;
    while (time < dashingCooldown)
    {
      time += Time.deltaTime;
      cooldownImage.fillAmount = 1 - time / dashingCooldown;
      yield return null;
    }
    cooldownImage.fillAmount = 0;
  }
}
