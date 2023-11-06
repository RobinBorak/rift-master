using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using Assets.HeroEditor4D.Common.Scripts.Enums;

public class PlayerCharacter : MonoBehaviour
{
  private Character4D character;
  public bool isMale = true;
  public bool loadFromStore = true;

  public string key = "PlayerCharacter";

  // Start is called before the first frame update
  void Start()
  {
    character = gameObject.GetComponent<Character4D>();
    if (loadFromStore)
    {
      Load();
    }
    else
    {
      // Character4D seems to cache equipment. This is a workaround to clear the cache.
      UpdateEquipment();
    }

    if (!isMale)
    {
      SetFemaleBody();
    }

  }

  private void SetFemaleBody()
  {
    //string hairId = "Common.Basic.Hair.ShortTail"; // Ponytail
    string hairId = "Common.Basic.Hair.Type17"; // 2 Stora Toffsar
    //string hairId = "Common.Basic.Hair.Type20"; // Dödskalle 2 små toffsar
    //string hairId = "Common.Basic.Hair.Type21"; // 2 små toffsar
    Color color = new Color(1f, 0.2f, 0.6f);
    var character4dHairItem = character.SpriteCollection.Hair.Find(i => i.Id == hairId);
    character.SetBody(character4dHairItem, BodyPart.Hair, color);

    //Common.Basic.Eyes.Type02
    string eyesId = "Common.Basic.Eyes.Type02";
    var character4dEyesItem = character.SpriteCollection.Eyes.Find(i => i.Id == eyesId);
    character.SetBody(character4dEyesItem, BodyPart.Eyes);

    string mouthId = "Common.Basic.Mouth.Smile";
    var character4dMouthItem = character.SpriteCollection.Mouth.Find(i => i.Id == mouthId);
    character.SetBody(character4dMouthItem, BodyPart.Mouth);
  }


  public void Save()
  {
    SerializedPlayerCharacter serializedPlayerCharacter = new SerializedPlayerCharacter(isMale);
    Store.Save(key, serializedPlayerCharacter);
  }

  private void Load()
  {
    SerializedPlayerCharacter serializedPlayerCharacter = (SerializedPlayerCharacter)Store.Load(key);
    if (serializedPlayerCharacter != null)
    {
      isMale = serializedPlayerCharacter.isMale;
    }
  }

  private void UpdateEquipment()
  {
    character.UnEquip(EquipmentPart.Helmet);
    character.UnEquip(EquipmentPart.Armor);
    character.UnEquip(EquipmentPart.MeleeWeapon1H);
  }

}
