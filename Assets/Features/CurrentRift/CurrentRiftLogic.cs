using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentRiftLogic : MonoBehaviour
{
  [SerializeField] private Image statusImage;
  private static CurrentRiftLogic instance;
  private CurrentRift currentRift;

  public int rift = 0;
  private float progress = 0;
  private bool complete = false;

  // Start is called before the first frame update
  void Start()
  {
    currentRift = FindObjectOfType<CurrentRift>();
    statusImage.fillAmount = progress;
    rift = currentRift.currentRift;
    FindObjectOfType<SelectRift>().SetRift(rift);
    DontDestroyOnLoad(gameObject);
  }

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void IncreaseSmallProgress()
  {
    IncreaseProgress(10f);
  }

  private void IncreaseProgress(float amount)
  {
    progress += amount;
    statusImage.fillAmount = progress / 100f;

    if (progress >= 100 && !complete)
    {
      Success();
    }
  }

  private void GoBackToTown()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    Destroy(gameObject);
  }

  private void Success()
  {
    Debug.Log("Rift complete, going back to town...");
    complete = true;
    PlayerRiftsStats.CreateOrUpdate(new RiftStats(rift, true));
    PlayerRiftsStats.SaveRiftsStats();
    currentRift.SetRiftDefault();
    Invoke("GoBackToTown", 5f);
  }
}
