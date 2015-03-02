using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Config {

  const string CONFIG_PATH = "Assets/Scripts/Config/";

  static JSONNode ReadFile (string filename) {
    string playerInitJSON = System.IO.File.ReadAllText(CONFIG_PATH + filename);
    return JSON.Parse (playerInitJSON);
  }

  static JSONNode _simulationConfig;
  static JSONNode SimulationConfig {
    get {
      if (_simulationConfig == null) {
        _simulationConfig = ReadFile("simulation.json");
      }
      return _simulationConfig;
    }
  }

  public static int StartGold {
    get {
      return SimulationConfig["player"]["start_gold"].AsInt;
    }
  }

  public static int StartSeconds {
    get {
      return SimulationConfig["start_seconds"].AsInt;
    }
  }

  public static float StartSpeed {
    get {
      return SimulationConfig["start_speed"].AsFloat;
    }
  }

  public static JSONArray Buildings {
    get {
      return SimulationConfig["buildings"].AsArray;
    }
  }

}

