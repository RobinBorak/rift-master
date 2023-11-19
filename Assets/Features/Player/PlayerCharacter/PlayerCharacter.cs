using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using Assets.HeroEditor4D.Common.Scripts.Enums;

public class PlayerCharacter : MonoBehaviour
{
  private Character4D character;
  public int usernameMaxLength = 16;

  public bool isMale = true;
  public bool loadFromStore = true;
  private string username = "";

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
    SerializedPlayerCharacter serializedPlayerCharacter = new SerializedPlayerCharacter(
      isMale,
      username
    );
    Store.Save(key, serializedPlayerCharacter);
  }

  private void Load()
  {
    SerializedPlayerCharacter serializedPlayerCharacter = (SerializedPlayerCharacter)Store.Load(key);
    if (serializedPlayerCharacter != null)
    {
      isMale = serializedPlayerCharacter.isMale;
      username = serializedPlayerCharacter.username;
    }
  }

  private void UpdateEquipment()
  {
    character.UnEquip(EquipmentPart.Helmet);
    character.UnEquip(EquipmentPart.Armor);
    character.UnEquip(EquipmentPart.MeleeWeapon1H);
  }

  public bool SetUsername(string username)
  {
    username = SanitizeString(username);
    username = username.Substring(0, Mathf.Min(username.Length, usernameMaxLength));

    if (username.Length < 3)
      return false;

    if (stringContainsBannedWords(username))
      return false;

    this.username = username;
    return true;
  }

  private string SanitizeString(string str)
  {
    //Only allow alphanumeric characters and spaces
    str = System.Text.RegularExpressions.Regex.Replace(str, @"[^a-zA-Z0-9 ]", "");
    // Remove leading and trailing spaces
    str = str.Trim();
    return str;
  }

  string[] bannedWords = new string[]{
    "rift", "borak", "robinborak", "rifthero", "r1ft", "r1fthero", 
    "4r5e", "5h1t", "5hit", "a55", "anal", "anus", "ar5e", "arrse", "arse", "ass", "ass-fucker", "asses", "assfucker", "assfukka", "asshole", "assholes", "asswhole", "a_s_s", "b!tch", "b00bs", "b17ch", "b1tch", "ballbag", "balls", "ballsack", "bastard", "beastial", "beastiality", "bellend", "bestial", "bestiality", "bi+ch", "biatch", "bitch", "bitcher", "bitchers", "bitches", "bitchin", "bitching", "bloody", "blow job", "blowjob", "blowjobs", "boiolas", "bollock", "bollok", "boner", "boob", "boobs", "booobs", "boooobs", "booooobs", "booooooobs", "breasts", "buceta", "bugger", "bum", "bunny fucker", "butt", "butthole", "buttmuch", "buttplug", "c0ck", "c0cksucker", "carpet muncher", "cawk", "chink", "cipa", "cl1t", "clit", "clitoris", "clits", "cnut", "cock", "cock-sucker", "cockface", "cockhead", "cockmunch", "cockmuncher", "cocks", "cocksuck", "cocksucked", "cocksucker", "cocksucking", "cocksucks", "cocksuka", "cocksukka", "cok", "cokmuncher", "coksucka", "coon", "cox", "crap", "cum", "cummer", "cumming", "cums", "cumshot", "cunilingus", "cunillingus", "cunnilingus", "cunt", "cuntlick", "cuntlicker", "cuntlicking", "cunts", "cyalis", "cyberfuc", "cyberfuck", "cyberfucked", "cyberfucker", "cyberfuckers", "cyberfucking", "d1ck", "damn", "dick", "dickhead", "dildo", "dildos", "dink", "dinks", "dirsa", "dlck", "dog-fucker", "doggin", "dogging", "donkeyribber", "doosh", "duche", "dyke", "ejaculate", "ejaculated", "ejaculates", "ejaculating", "ejaculatings", "ejaculation", "ejakulate", "f u c k", "f u c k e r", "f4nny", "fag", "fagging", "faggitt", "faggot", "faggs", "fagot", "fagots", "fags", "fanny", "fannyflaps", "fannyfucker", "fanyy", "fatass", "fcuk", "fcuker", "fcuking", "feck", "fecker", "felching", "fellate", "fellatio", "fingerfuck", "fingerfucked", "fingerfucker", "fingerfuckers", "fingerfucking", "fingerfucks", "fistfuck", "fistfucked", "fistfucker", "fistfuckers", "fistfucking", "fistfuckings", "fistfucks", "flange", "fook", "fooker", "fuck", "fucka", "fucked", "fucker", "fuckers", "fuckhead", "fuckheads", "fuckin", "fucking", "fuckings", "fuckingshitmotherfucker", "fuckme", "fucks", "fuckwhit", "fuckwit", "fudge packer", "fudgepacker", "fuk", "fuker", "fukker", "fukkin", "fuks", "fukwhit", "fukwit", "fux", "fux0r", "f_u_c_k", "gangbang", "gangbanged", "gangbangs", "gaylord", "gaysex", "goatse", "God", "god-dam", "god-damned", "goddamn", "goddamned", "hardcoresex", "hell", "heshe", "hoar", "hoare", "hoer", "homo", "hore", "horniest", "horny", "hotsex", "jack-off", "jackoff", "jap", "jerk-off", "jism", "jiz", "jizm", "jizz", "kawk", "knob", "knobead", "knobed", "knobend", "knobhead", "knobjocky", "knobjokey", "kock", "kondum", "kondums", "kum", "kummer", "kumming", "kums", "kunilingus", "l3i+ch", "l3itch", "labia", "lust", "lusting", "m0f0", "m0fo", "m45terbate", "ma5terb8", "ma5terbate", "masochist", "master-bate", "masterb8", "masterbat*", "masterbat3", "masterbate", "masterbation", "masterbations", "masturbate", "mo-fo", "mof0", "mofo", "mothafuck", "mothafucka", "mothafuckas", "mothafuckaz", "mothafucked", "mothafucker", "mothafuckers", "mothafuckin", "mothafucking", "mothafuckings", "mothafucks", "mother fucker", "motherfuck", "motherfucked", "motherfucker", "motherfuckers", "motherfuckin", "motherfucking", "motherfuckings", "motherfuckka", "motherfucks", "muff", "mutha", "muthafecker", "muthafuckker", "muther", "mutherfucker", "n1gga", "n1gger", "nazi", "nigg3r", "nigg4h", "nigga", "niggah", "niggas", "niggaz", "nigger", "niggers", "nob", "nob jokey", "nobhead", "nobjocky", "nobjokey", "numbnuts", "nutsack", "orgasim", "orgasims", "orgasm", "orgasms", "p0rn", "pawn", "pecker", "penis", "penisfucker", "phonesex", "phuck", "phuk", "phuked", "phuking", "phukked", "phukking", "phuks", "phuq", "pigfucker", "pimpis", "piss", "pissed", "pisser", "pissers", "pisses", "pissflaps", "pissin", "pissing", "pissoff", "poop", "porn", "porno", "pornography", "pornos", "prick", "pricks", "pron", "pube", "pusse", "pussi", "pussies", "pussy", "pussys", "rectum", "retard", "rimjaw", "rimming", "s hit", "s.o.b.", "sadist", "schlong", "screwing", "scroat", "scrote", "scrotum", "semen", "sex", "sh!+", "sh!t", "sh1t", "shag", "shagger", "shaggin", "shagging", "shemale", "shi+", "shit", "shitdick", "shite", "shited", "shitey", "shitfuck", "shitfull", "shithead", "shiting", "shitings", "shits", "shitted", "shitter", "shitters", "shitting", "shittings", "shitty", "skank", "slut", "sluts", "smegma", "smut", "snatch", "son-of-a-bitch", "spac", "spunk", "s_h_i_t", "t1tt1e5", "t1tties", "teets", "teez", "testical", "testicle", "tit", "titfuck", "tits", "titt", "tittie5", "tittiefucker", "titties", "tittyfuck", "tittywank", "titwank", "tosser", "turd", "tw4t", "twat", "twathead", "twatty", "twunt", "twunter", "v14gra", "v1gra", "vagina", "viagra", "vulva", "w00se", "wang", "wank", "wanker", "wanky", "whoar", "whore", "willies", "willy", "xrated", "xxx"
  };
  public bool stringContainsBannedWords(string str)
  {
    foreach (string bannedWord in bannedWords)
    {
      if (str.ToLower().Contains(bannedWord))
      {
        return true;
      }
    }
    return false;

  }

  public string Username
  {
    get
    {
      return username;
    }
  }
}
