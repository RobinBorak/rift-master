using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
  private static PlayerInventory instance;

  // Is there a better way?
  public RiftItems AllRiftItems; // This is a reference to the RiftItems scriptable object

  private static string key = "PlayerInventory";
  private SerializedInventory serializedInventory;
  private int gold = 0;
  public List<PlayerInventoryItem> playerInventoryItems = new List<PlayerInventoryItem>();

  public delegate void OnGoldChangeDelegate(int amount);
  public static event OnGoldChangeDelegate onGoldChangeDelegate;

  public delegate void OnItemChangeDelegate();
  public static event OnItemChangeDelegate onItemChangeDelegate;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject);

      serializedInventory = (SerializedInventory)Store.Load(key);

      if (serializedInventory == null)
        serializedInventory = new SerializedInventory();

      gold = serializedInventory.gold;


      if (serializedInventory.items != null)
      {

        foreach (SerializedPlayerInventoryItem item in serializedInventory.items)
        {
          AddItemFromSavedInventory(item.id, item.quantity);
        }
      }

      PlayerInventory.onGoldChangeDelegate += Save;
      RiftItem.useItemDelegate += (int id) => RemoveItem(id, 1);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void Save(int amount)
  {
    Store.Save(key, new SerializedInventory(
      gold,
      playerInventoryItems
    ));
  }


  public void AddGold(int amount)
  {
    gold += amount;
    onGoldChangeDelegate?.Invoke(amount);
  }

  private void AddItemFromSavedInventory(int id, int quantity)
  {
    // Remove pointer to RiftItem
    RiftItem item = ScriptableObject.CreateInstance<RiftItem>();
    item.Init(AllRiftItems.GetRiftItem(id));
    item.quantity = quantity;
    playerInventoryItems.Add(new PlayerInventoryItem(item));
  }

  public void AddItem(PlayerInventoryItem item)
  {
    if (item.item.stackable)
    {
      PlayerInventoryItem playerInventoryItem = playerInventoryItems.Find(x => x.item.id == item.item.id);
      if (playerInventoryItem != null)
      {
        playerInventoryItem.item.quantity += item.item.quantity;
      }
      else
      {
        playerInventoryItems.Add(item);
      }
    }
    else
    {
      playerInventoryItems.Add(item);
    }
    onItemChangeDelegate?.Invoke();
    Save(0);
  }

  public void RemoveItem(int id, int quantity)
  {
    RiftItem item = AllRiftItems.GetRiftItem(id);
    PlayerInventoryItem playerInventoryItem = playerInventoryItems.Find(x => x.item.id == item.id);

    if (playerInventoryItem != null)
    {
      playerInventoryItem.item.quantity -= quantity;
      if (playerInventoryItem.item.quantity <= 0)
      {
        playerInventoryItems.Remove(playerInventoryItem);
      }
    }
    onItemChangeDelegate?.Invoke();
    Save(0);
  }

  public void RemoveItem(PlayerInventoryItem item)
  {
    PlayerInventoryItem playerInventoryItem = playerInventoryItems.Find(x => x.item.id == item.item.id);
    if (playerInventoryItem != null)
    {
      playerInventoryItems.Remove(playerInventoryItem);
    }
    onItemChangeDelegate?.Invoke();
    Save(0);
  }

  public RiftItem GetInventoryItem(int id)
  {
    PlayerInventoryItem playerInventoryItem = playerInventoryItems.Find(x => x.item.id == id);
    if (playerInventoryItem != null)
    {
      return playerInventoryItem.item;
    }
    return null;
  }

  public RiftItem GetItem(int id)
  {
    return AllRiftItems.GetRiftItem(id);
  }

  //Getters and Setters
  public int Gold
  {
    get { return gold; }
  }

  public List<PlayerInventoryItem> PlayerInventoryItems
  {
    get { return playerInventoryItems; }
  }

}
