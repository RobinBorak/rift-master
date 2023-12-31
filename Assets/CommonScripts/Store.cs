using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Store
{

  public static void Save(string key, object value)
  {
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Create(Application.persistentDataPath + "/" + key + ".gd");
    bf.Serialize(file, value);
    file.Close();
  }

  public static object Load(string key)
  {
    if (File.Exists(Application.persistentDataPath + "/" + key + ".gd"))
    {
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(Application.persistentDataPath + "/" + key + ".gd", FileMode.Open);
      object value = bf.Deserialize(file);
      file.Close();
      return value;
    }
    return null;
  }
}
