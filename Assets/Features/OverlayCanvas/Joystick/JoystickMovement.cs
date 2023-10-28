using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickMovement : MonoBehaviour
{

  [SerializeField] private GameObject joystick;
  [SerializeField] private GameObject joystickBg;
  public Vector2 joystickVector;
  private Vector2 joystickTouchPos;
  private Vector2 joystickOriginalPos;
  private float joystickRadius;

  private Transform canvasZero;


  // Start is called before the first frame update
  void Start()
  {
    canvasZero = FindObjectOfType<Player>().transform;
    joystickOriginalPos = joystickBg.transform.localPosition;
    joystickRadius = joystickBg.GetComponent<RectTransform>().sizeDelta.y / 4;

    InitJoystick();
  }

  private void InitJoystick()
  {
    //Init event triggers
    EventTrigger joystickEventTrigger = gameObject.GetComponent<EventTrigger>();
    EventTrigger.Entry joystickEntry = new EventTrigger.Entry();
    joystickEntry.eventID = EventTriggerType.PointerDown;
    joystickEntry.callback.AddListener((data) => { PointerDown(); });
    joystickEventTrigger.triggers.Add(joystickEntry);

    EventTrigger.Entry joystickDragEntry = new EventTrigger.Entry();
    joystickDragEntry.eventID = EventTriggerType.Drag;
    joystickDragEntry.callback.AddListener((data) => { Drag(data); });
    joystickEventTrigger.triggers.Add(joystickDragEntry);

    EventTrigger.Entry joystickUpEntry = new EventTrigger.Entry();
    joystickUpEntry.eventID = EventTriggerType.PointerUp;
    joystickUpEntry.callback.AddListener((data) => { PointerUp(); });
    joystickEventTrigger.triggers.Add(joystickUpEntry);

  }

  public void PointerDown()
  {
    Vector2 mouse = getLeftTouchIndex();
    joystickBg.transform.localPosition = mouse;
    joystick.transform.localPosition = mouse;
    joystickTouchPos = mouse;
  }



  public void Drag(BaseEventData baseEventData)
  {
    Vector2 dragPos = getLeftTouchIndex();
    joystickVector = (dragPos - joystickTouchPos).normalized;

    // * 30 to make it more sensitive
    float joystickDistance = Vector2.Distance(dragPos, joystickTouchPos) * 30;

    if (joystickDistance < joystickRadius)
    {
      joystick.transform.localPosition = joystickTouchPos + joystickVector * joystickDistance;
    }
    else
    {
      joystick.transform.localPosition = joystickTouchPos + joystickVector * joystickRadius;
    }
  }
  Vector2 getLeftTouchIndex()
  {
    int touchesLength = Input.touches.Length;
    Vector2 mouse = Input.mousePosition;

    int leftTouchIndex = -1;
    for (int i = 0; i < Input.touches.Length; i++)
    {
      if (Input.touches[i].position.x < Screen.width / 2)
      {
        leftTouchIndex = i;
      }
    }
    if (leftTouchIndex > -1)
      mouse = Input.touches[leftTouchIndex].position;

    // Calculate the position of the mouse in the canvas
    Vector2 screenPoint = Camera.main.ScreenToWorldPoint(mouse);
    Vector2 calculated = new Vector2(
      screenPoint.x + joystickOriginalPos.x - canvasZero.position.x,
      screenPoint.y + joystickOriginalPos.y - canvasZero.position.y
      );
    return calculated;
  }

  public void PointerUp()
  {
    joystickVector = Vector2.zero;
    joystickBg.transform.localPosition = joystickOriginalPos;
    joystick.transform.localPosition = joystickOriginalPos;
  }

}
