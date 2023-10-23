using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentRiftLogic : MonoBehaviour
{
  [SerializeField] private Image statusImage;
  private static CurrentRiftLogic instance;

  private float progress = 0;
  private bool complete = false;

  // Start is called before the first frame update
  void Start()
  {
    statusImage.fillAmount = progress;
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
      // Rift is complete
      complete = true;
      Debug.Log("Rift complete, going back to town...");
      Invoke("GoBackToTown", 5f);
    }
  }

  private void GoBackToTown()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    Destroy(gameObject);
  }
}
