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
  private PlayerCharacter playerCharacter;

  public int rift = 0;
  private float progress = 0;
  private bool complete = false;
  private int timeToComplete = 120;
  private int timeLeft = 0;
  private int expForRiftCompletion = 50;

  public bool isDoneLoading = false;

  public delegate void OnRiftComplete();
  public static event OnRiftComplete onRiftCompleteCallback;

  public delegate void OnRiftFailed();
  public static event OnRiftFailed onRiftFailedCallback;

  // Start is called before the first frame update
  void Start()
  {
    playerCharacter = FindObjectOfType<PlayerCharacter>();
    timeLeft = timeToComplete;

    statusImage.fillAmount = progress;
    timerImage.fillAmount = timeLeft / (float)timeToComplete;

    currentRift = FindObjectOfType<CurrentRift>();
    rift = currentRift != null ? currentRift.currentRift : 0;
    FindObjectOfType<SelectRift>().SetRift(rift);

    InvokeRepeating("UpdateTimeLeft", 1f, 1f);

    isDoneLoading = true;
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
      onRiftFailedCallback?.Invoke();
      FindObjectOfType<CurrentRift>().SetRiftDefault();
      Invoke("GoBackToTown", 10.5f);
      complete = true;

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
    if (progress <= 0)
      return;
    IncreaseProgress(-10f);
  }

  private void IncreaseProgress(float amount)
  {
    if (complete)
      return;

    progress += amount;
    statusImage.fillAmount = progress / 100f;

    if (progress >= 100)
    {
      Success();
    }
  }

  private void GoBackToTown()
  {
    PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
    PlayerStats playerStats = FindObjectOfType<PlayerStats>();
    playerHealth.HealToFull();
    playerStats.Save();
    UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    Destroy(gameObject);
  }

  private void Success()
  {
    Debug.Log("Rift complete, going back to town...");
    onRiftCompleteCallback?.Invoke();
    complete = true;
    PlayerRiftsStats.CreateOrUpdate(new RiftStats(rift, true, timeToComplete - timeLeft));
    PlayerRiftsStats.SaveRiftsStats();
    currentRift.SetRiftDefault();
    FindObjectOfType<Player>().GainExp(expForRiftCompletion * rift);
    Invoke("GoBackToTown", 10.5f);
    Highscore.UploadNewEntry(playerCharacter.Username, rift);
  }

  //Getters and setters
  public int Rift
  {
    get { return rift; }
  }

}
