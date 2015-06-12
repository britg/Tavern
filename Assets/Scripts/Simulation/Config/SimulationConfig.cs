using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

[System.Serializable]
public class SimulationConfig {

  public float updateIntervalSeconds = 1;
  public float initialSpeed = 1;
  public int initialGold = 1000;
  public string[] startBuildings = new string[]{ "tavern" };
  
  const string CONFIG_PATH = "Assets/Scripts/Simulation/Config";


  public void LoadModels () {
    foreach (string dir in Directory.GetDirectories(CONFIG_PATH)) {
      LoadDirectory(dir);
    }
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

    if (ext == ".json") {
      string contents = File.ReadAllText(filename);
      ParseContents(contents);
    } else {
    }

  }

  public void ParseContents (string json) {
    var parsed = JSON.Parse(json);
    var type = parsed["type"].Value;


    switch (type) {
      case Resource.type:
        Resource.Cache(parsed);
      break;
      case AdventurerClass.type:
        AdventurerClass.Cache(parsed);
      break;
    }

  }

  public Player LoadPlayer () {
    var playerCreator = new PlayerCreator(this);
    return playerCreator.Create();
  }

}