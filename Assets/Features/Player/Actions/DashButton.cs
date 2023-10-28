using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DashButton : MonoBehaviour
{
  private PlayerMovement playerMovement;
  // Start is called before the first frame update
  void Start()
  {
    playerMovement = FindObjectOfType<PlayerMovement>();
    InitDashButton();
  }

  private void InitDashButton()
  {
    //On attackButton pointer down
    EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
    EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
    pointerDownEntry.eventID = EventTriggerType.PointerDown;
    pointerDownEntry.callback.AddListener((data) => { playerMovement.Dash(); });
    trigger.triggers.Add(pointerDownEntry);
  }
}
