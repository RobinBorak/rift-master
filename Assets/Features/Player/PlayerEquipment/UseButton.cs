using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseButton : MonoBehaviour
{
  public RiftItem item;

  public delegate void OnUseItemDelegate();
  public static event OnUseItemDelegate onUseItemDelegate;

  public void UseItem()
  {
    item.Use();
    onUseItemDelegate?.Invoke();
  }
}
