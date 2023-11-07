using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using Assets.HeroEditor4D.Common.Scripts.Enums;

public class SelectPlayerCharacter : MonoBehaviour
{

  void Awake()
  {
    string key = "PlayerCharacter";
    SerializedPlayerCharacter serializedPlayerCharacter = (SerializedPlayerCharacter)Store.Load(key);
    if (serializedPlayerCharacter != null)
    {
      GoToTown();
    }
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

      if (hit.collider != null)
      {
        PlayerCharacter playerCharacter = hit.collider.GetComponent<PlayerCharacter>();
        playerCharacter.Save();
        Character4D character4D = hit.collider.GetComponent<Character4D>();
        character4D.AnimationManager.SetState(CharacterState.Dance);
        Invoke("GoToTown", 2f);
      }
    }
  }


  private void GoToTown()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene(1);
  }

}
