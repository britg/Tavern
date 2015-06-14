﻿using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class SimulationConfig {

  public const string type = "Simulation";

  Simulation sim;

  // Core Config

  // Time
  public float updateIntervalSeconds;
  public int startSeconds;
  public int dayStartHour;
  public int nightStartHour;
  public float initialSpeed;

  // Player
  public int initialGold;
  public List<String> startBuildings = new List<String>();

  // Adventurer
  public int adventurerDefaultLevel;
  public int adventurerDefaultExp;
  public float adventurerDefaultSpeed;


  // Model config
  const string CONFIG_PATH = "Assets/Scripts/Simulation/Config";
  const string EXT = ".json";

  public SimulationConfig (Simulation _sim) {
    sim = _sim;
  }

  public void LoadSelf (JSONNode json) {
    Debug.Log ("Loading self " + json.ToString());
    updateIntervalSeconds = json["updateIntervalSeconds"].AsFloat;
    initialSpeed = json["initialSpeed"].AsFloat;
    initialGold = json["initialGold"].AsInt;
    startSeconds = json["startSeconds"].AsInt;
    foreach(JSONNode arrItem in json["startBuildings"].AsArray) {
      startBuildings.Add(arrItem.Value);
    }

    adventurerDefaultLevel = json["adventurerDefaultLevel"].AsInt;
    adventurerDefaultExp = json["adventurerDefaultExp"].AsInt;
    adventurerDefaultSpeed = json["adventurerDefaultSpeed"].AsFloat;
  }

  public void LoadModels () {
    LoadDirectory(CONFIG_PATH);
  }

  public void LoadDirectory (string dir) {

    foreach (string subdir in Directory.GetDirectories(dir)) {
      LoadDirectory(subdir);
    }

    foreach (string filename in Directory.GetFiles(dir)) {
      LoadFile(filename);
    }
  }

  public void LoadFile (string filename) {
    var fileInfo = new FileInfo(filename);
    var ext = fileInfo.Extension;

    if (ext == EXT) {
      string contents = File.ReadAllText(filename);
      ParseContents(contents);
    } else {
    }

  }

  public void ParseContents (string json) {
    var parsed = JSON.Parse(json);
    var type = parsed["type"].Value;

    switch (type) {
      case ResourceType.type:
        ResourceType.Cache(parsed);
      break;
      case AdventurerClass.type:
        AdventurerClass.Cache(parsed);
      break;
      case BuildingType.type:
        BuildingType.Cache(parsed);
      break;
      case QuestType.type:
        QuestType.Cache(parsed);
      break;
      case SimulationConfig.type:
        LoadSelf(parsed);
      break;
      case Name.type:
        Name.Cache(parsed);
        break;
      default:
        Debug.LogWarning(string.Format("Failed to load {0} {1}", parsed["type"], parsed["key"]));
      break;
    }

  }

  public void LoadExistingPlayer () {
    
  }

  public void CreatePlayer () {
    var playerCreator = new PlayerCreator(sim);
    playerCreator.Create();
  }

}