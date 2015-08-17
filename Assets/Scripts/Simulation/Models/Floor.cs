using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Floor {

  public const string type = "Floor";

  public static Dictionary<int, Floor> all = new Dictionary<int, Floor>();

  public int num;
  public FloorTemplate floorTemplate;

  public Dictionary<string, float> consumableChances {
    get {
      return floorTemplate.consumableChances;
    }
  }

  public static Floor GetFloor (int num) {
    return all[num];
  }

  public static void Cache (JSONNode json) {
    var floor = new Floor(json);
    all[floor.num] = floor;
    Debug.Log("Loaded floor " + floor.num);
  }

  public Floor () {

  }

  public Floor (JSONNode json) {
    num = json["number"].AsInt;
    floorTemplate = FloorTemplate.all[json["template"].Value];
  }


  public string Name () {
    return string.Format("{0} {1} Floor", floorTemplate.name, NumberUtilities.AddOrdinal(num));
  }

  public Mob RandomMob () {
    return floorTemplate.RandomMob();
  }

  public string RandomAtmosphereText () {
    return floorTemplate.RandomAtmosphereText();
  }

  public Interactible RandomInteractible () {
    return floorTemplate.RandomInteractible();
  }

  
}
