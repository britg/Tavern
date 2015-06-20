using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Rarity {

  public const string type = "Rarity";

  public string Key { get; set; }
  public string Name { get; set; }

  public static Dictionary<string, Rarity> all = new Dictionary<string, Rarity>();

  public static void Cache (JSONNode json) {
    var rarity = new Rarity(json);
    all[rarity.Key] = rarity;
    Debug.Log("Loaded rarity type " + rarity.Name);
  }

  public Rarity () {

  }

  public Rarity (JSONNode json) {
    Key = json["key"].Value;
    Name = json["name"].Value;
  }

}
