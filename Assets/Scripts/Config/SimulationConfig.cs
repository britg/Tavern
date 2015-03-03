using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class SimulationConfig {

  const string CONFIG_PATH = "Assets/Scripts/Config/config.json";

  static SimulationConfig _simulationConfig;
  public static SimulationConfig Instance {
    get {
      if (_simulationConfig == null) {
        string jsonstring = System.IO.File.ReadAllText(CONFIG_PATH);
        _simulationConfig = JsonConvert.DeserializeObject<SimulationConfig>(jsonstring);
      }
      return _simulationConfig;
    }
  }

  public TimeConfig TimeConfig;
  public PlayerConfig PlayerConfig;
  public BuildingConfig BuildingConfig;
  public CommodityConfig CommodityConfig;

  public override string ToString () {
    return JsonConvert.SerializeObject(this);
  }

}