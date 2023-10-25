using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentRiftLogic : MonoBehaviour
{
  private static CurrentRiftLogic instance;

  [SerializeField] private Image statusImage;
  [SerializeField] private Image timerImage;
  private CurrentRift currentRift;

  public int rift = 0;
  private float progress = 0;
  private bool complete = false;
  private int timeToComplete = 60;
  private int timeLeft = 0;

  // Start is called before the first frame update
  void Start()
  {
    timeLeft = timeToComplete;

    statusImage.fillAmount = progress;
    timerImage.fillAmount = timeLeft / (float)timeToComplete;

    currentRift = FindObjectOfType<CurrentRift>();
    rift = currentRift != null ? currentRift.currentRift : 0;
    FindObjectOfType<SelectRift>().SetRift(rift);

    InvokeRepeating("UpdateTimeLeft", 1f, 1f);
  }

  private void UpdateTimeLeft()
  {
    if (complete)
    {
      return;
    }

    timeLeft--;
    timerImage.fillAmount = timeLeft / (float)timeToComplete;
    if (timeLeft <= 0)
    {
      Debug.Log("Rift failed, going back to town...");
      FindObjectOfType<CurrentRift>().SetRiftDefault();
      GoBackToTown();
    }
  }

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject);
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

  public void DecreaseSmallProgress()
  {
    IncreaseProgress(-10f);
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
    PlayerRiftsStats.CreateOrUpdate(new RiftStats(rift, true, timeToComplete - timeLeft));
    PlayerRiftsStats.SaveRiftsStats();
    currentRift.SetRiftDefault();
    Invoke("GoBackToTown", 1f);
  }

  //Getters and setters
  public int Rift
  {
    get { return rift; }
  }

}
