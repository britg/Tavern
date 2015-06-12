using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class AdventurerClass {

  public const string type = "AdventurerClass";

  public string Key { get; set; }
  public string Name { get; set; }

  public static Dictionary<string, AdventurerClass> all = new Dictionary<string, AdventurerClass>();

  public static void Cache (JSONNode json) {
    var adventurerClass = new AdventurerClass(json);
    all[adventurerClass.Key] = adventurerClass;
    Debug.Log ("Loaded resource type " + adventurerClass.Name);
  }

  public AdventurerClass () {

  }

  public AdventurerClass (JSONNode json) {
    Key = json["key"].Value;
    Name = json["name"].Value;
  }

}
