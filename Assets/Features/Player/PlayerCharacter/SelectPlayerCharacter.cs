using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using Assets.HeroEditor4D.Common.Scripts.Enums;
using UnityEngine.UI;

public class SelectPlayerCharacter : MonoBehaviour
{

  private string key = "PlayerCharacter";
  PlayerCharacter selectedPlayerCharacter;
  Character4D character4D;
  [SerializeField] private TextMeshProUGUI usernameText;

  [SerializeField] private GameObject usernameCanvas;
  [SerializeField] private GameObject characterCanvas;
  [SerializeField] private GameObject bannedWords;

  private Vector3 originalCameraPos;

  void Start()
  {
    originalCameraPos = Camera.main.transform.position;
    SerializedPlayerCharacter serializedPlayerCharacter = (SerializedPlayerCharacter)Store.Load(key);
    if (serializedPlayerCharacter != null)
    {
      if (!string.IsNullOrEmpty(serializedPlayerCharacter.username))
      {
        Debug.Log("Username: " + serializedPlayerCharacter.username);
        GoToTown();
      }
    }
  }

  private bool isTransitioning = false;
  private void StopTransitioning()
  {
    isTransitioning = false;
  }
  void Update()
  {
    if (isTransitioning)
    {
      return;
    }
    if (Input.GetMouseButtonDown(0))
    {
      RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
      if (selectedPlayerCharacter == null && hit.collider != null && hit.collider.GetComponent<PlayerCharacter>() != null)
      {
        isTransitioning = true;
        selectedPlayerCharacter = hit.collider.GetComponent<PlayerCharacter>();
        character4D = hit.collider.GetComponent<Character4D>();
        StartCoroutine(resizeRoutine(Camera.main.orthographicSize, 0.5f, 1.5f));
        Vector3 newPos = new Vector3(
          selectedPlayerCharacter.transform.position.x + 0.5f,
          selectedPlayerCharacter.transform.position.y,
          selectedPlayerCharacter.transform.position.z);
        StartCoroutine(LerpToRoutine(Camera.main.transform.position, newPos, 1.5f));
        ToggleCanvases();
        Invoke("StopTransitioning", 2f);
      }
    }
  }

  public void BackButton()
  {
    if (selectedPlayerCharacter != null)
    {
      isTransitioning = true;
      selectedPlayerCharacter = null;
      StartCoroutine(resizeRoutine(Camera.main.orthographicSize, 1f, 1.5f));
      StartCoroutine(LerpToRoutine(Camera.main.transform.position, originalCameraPos, 1.5f));
      ToggleCanvases();
      Invoke("StopTransitioning", 2f);
    }
  }


  private void ToggleCanvases()
  {
    if (characterCanvas.activeSelf)
    {
      ToggleCharacterCanvas();
      Invoke("ToggleUsernameCanvas", 2f);
    }
    else
    {
      ToggleUsernameCanvas();
      Invoke("ToggleCharacterCanvas", 2f);
    }
  }

  private void ToggleUsernameCanvas()
  {
    usernameCanvas.SetActive(!usernameCanvas.activeSelf);
  }

  private void ToggleCharacterCanvas()
  {
    characterCanvas.SetActive(!characterCanvas.activeSelf);
  }

  private IEnumerator LerpToRoutine(Vector3 _oldPos, Vector3 _newPos, float time)
  {
    float elapsed = 0;
    Vector3 oldPos = new Vector3(_oldPos.x, _oldPos.y, -10f);
    Vector3 newPos = new Vector3(_newPos.x, _newPos.y, -10f);
    while (elapsed <= time)
    {
      elapsed += Time.deltaTime;
      float t = Mathf.Clamp01(elapsed / time);

      Camera.main.transform.position = Vector3.Lerp(oldPos, newPos, t);
      yield return null;
    }
  }

  private IEnumerator resizeRoutine(float oldSize, float newSize, float time)
  {
    float elapsed = 0;
    while (elapsed <= time)
    {
      elapsed += Time.deltaTime;
      float t = Mathf.Clamp01(elapsed / time);

      Camera.main.orthographicSize = Mathf.Lerp(oldSize, newSize, t);
      yield return null;
    }
  }

  public void SetUsername()
  {
    bannedWords.SetActive(false);


    bool usernameIsValid = selectedPlayerCharacter.SetUsername(usernameText.text);
    if (!usernameIsValid)
    {
      if (selectedPlayerCharacter.stringContainsBannedWords(usernameText.text))
        bannedWords.SetActive(true);
    }
    else
    {
      character4D.AnimationManager.SetState(CharacterState.Dance);
      selectedPlayerCharacter.Save();
      Invoke("GoToTown", 2f);
    }
  }



  private void GoToTown()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene(1);
  }

}
