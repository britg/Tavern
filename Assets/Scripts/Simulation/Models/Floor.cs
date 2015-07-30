using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class FloorTemplate {

  public const string type = "FloorTemplate";

  public string name;
  public int minFloor;
  public int maxFloor;
  public List<string> atmosphereText;
  public List<string> mobKeys;

  public static Dictionary<int, FloorTemplate> all = new Dictionary<int, FloorTemplate>();

  public static void Cache (JSONNode json) {
    var floor = new FloorTemplate(json);
    all[floor.minFloor] = floor;
    Debug.Log("Loaded floor " + floor.minFloor + " - " + floor.maxFloor);
  }

  public FloorTemplate () {

  }

  public FloorTemplate (JSONNode json) {

    name = json["name"].Value;

    var floors = json["floors"].AsArray;
    minFloor = floors[0].AsInt;
    maxFloor = floors[1].AsInt;
    atmosphereText = new List<string>();
    mobKeys = new List<string>();

    var atmArr = json["atmosphere"].AsArray;
    foreach (JSONNode atmNode in atmArr) {
      atmosphereText.Add(atmNode.Value);
    }

    var mobArr = json["mobs"].AsArray;
    foreach (JSONNode mobNode in mobArr) {
      mobKeys.Add(mobNode.Value);
    }

  }

  public static FloorTemplate GetFloor (int floorNum) {
    foreach (KeyValuePair<int, FloorTemplate> pair in all) {
      var floor = pair.Value;
      if (floor.minFloor <= floorNum && floor.maxFloor >= floorNum) {
        return floor;
      }
    }

    return null;
  }

  public string RandomAtmosphereText () {
    int rand = Random.Range(0, atmosphereText.Count - 1);
    return atmosphereText[rand];
  }

  public Mob RandomMob () {
    int rand = Random.Range(0, mobKeys.Count - 1);
    string key = mobKeys[rand];
    MobTemplate template = MobTemplate.all[key];
    Mob mob = Mob.FromTemplate(template);

    return mob;
  }

  public Interactible RandomInteractible () {
    var interactible = new Interactible();
    interactible.name = "skeletal remains";
    return interactible;
  }




}
