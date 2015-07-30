using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class MobTemplate {
  
  public const string type = "MobTemplate";

  public string Key;
  public string name;

  public int minHitpoints;
  public int maxHitpoints;

  public int minAp;
  public int maxAp;

  public int minDps;
  public int maxDps;

  public static Dictionary<string, MobTemplate> all = new Dictionary<string, MobTemplate>();

  public static void Cache (JSONNode json) {
    var mobTemplate = new MobTemplate(json);
    all[mobTemplate.Key] = mobTemplate;
    Debug.Log("Loaded mob template " + mobTemplate.Key);
  }

  public MobTemplate () {

  }

  public MobTemplate (JSONNode json) {
    Key = json["key"].Value;
    name = json["name"].Value;
    var hpArr = json["hitpoints"].AsArray;
    minHitpoints = hpArr[0].AsInt;
    maxHitpoints = hpArr[1].AsInt;
  }
}
