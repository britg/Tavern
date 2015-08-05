using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class ConsumableType {
  public const string type = "ConsumableType";

  public string Key { get; set; }
  public string Name { get; set; }

  public static Dictionary<string, ConsumableType> all = new Dictionary<string, ConsumableType>();

  public static void Cache (JSONNode json) {
    var consumableType = new ConsumableType(json);
    all[consumableType.Key] = consumableType;
    Debug.Log("Loaded consumable type " + consumableType.Name);
  }

  public ConsumableType () {

  }

  public ConsumableType (JSONNode json) {
    Key = json["key"].Value;
    Name = json["name"].Value;
  }
}
