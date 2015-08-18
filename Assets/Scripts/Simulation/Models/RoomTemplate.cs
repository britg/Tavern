using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class RoomTemplate {

  public const string type = "RoomTemplate";

  public static Dictionary<string, RoomTemplate> all = new Dictionary<string, RoomTemplate>();

  public string key;
  public Floor floor;
  public Dictionary<string, float> content;

  public static void Cache (JSONNode json) {
    var roomTemplate = new RoomTemplate(json);
    all[roomTemplate.key] = roomTemplate;
    Debug.Log("Loading room template");
  }

  public RoomTemplate () {

  }

  public RoomTemplate (JSONNode json) {

    key = json["key"].Value;
    CascadeContent(json["content"].AsArray);

  }

  void CascadeContent (JSONArray contentJson) {
    // Cascade content from Floor
    content = floor.content;
    foreach (JSONNode item in contentJson) {
      var key = item["key"].Value;
      var chance = item["chance"].AsFloat;
      content[key] = chance;
    }
  }


}
