using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class BuildingType {

  public const string type = "BuildingType";

  public string Key { get; set; }
  public string Name { get; set; }

  public static Dictionary<string, BuildingType> all = new Dictionary<string, BuildingType>();

  public static void Cache (JSONNode json) {
    var buildingType = new BuildingType(json);
    all[buildingType.Key] = buildingType;
    Debug.Log ("Loaded building type " + buildingType.Name);
  }

  public BuildingType () {

  }

  public BuildingType (JSONNode json) {
    Key = json["key"].Value;
    Name = json["name"].Value;
  }


}
