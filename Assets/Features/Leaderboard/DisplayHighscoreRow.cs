using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Models;

public class DisplayHighscoreRow : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI usernameText;
  [SerializeField] private TextMeshProUGUI scoreText;
  [SerializeField] private GameObject SelectedFrame;
  private Entry entry;

  public void SetEntry(Entry entry)
  {
    this.entry = entry;

    usernameText.text = entry.Username;
    scoreText.text = entry.Score.ToString();

    if (entry.IsMine())
      SelectedFrame.SetActive(true);
  }
}
