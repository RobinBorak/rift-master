using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour
{
  private PlayerCombat playerCombat;
  // Start is called before the first frame update
  void Start()
  {
    playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
    InitAttackButton();
  }

  private void InitAttackButton()
  {
    //On attackButton pointer down
    EventTrigger attackTrigger = gameObject.GetComponent<EventTrigger>();
    EventTrigger.Entry attackPointerDownEntry = new EventTrigger.Entry();
    attackPointerDownEntry.eventID = EventTriggerType.PointerDown;
    attackPointerDownEntry.callback.AddListener((data) => { playerCombat.Attack(); });
    attackTrigger.triggers.Add(attackPointerDownEntry);
  }
}
