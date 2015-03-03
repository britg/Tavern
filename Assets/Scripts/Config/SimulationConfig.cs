using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class SimulationConfig {

  const string CONFIG_PATH = "Assets/Scripts/Config/";

  static SimulationConfig _simulationConfig;
  public static SimulationConfig Instance {
    get {
      if (_simulationConfig == null) {
        _simulationConfig = new SimulationConfig();
      }
      return _simulationConfig;
    }
  }

  public static T ReadConfig<T> (string filename) {
    string jsonstring = System.IO.File.ReadAllText(CONFIG_PATH + filename);
    return (T)JsonConvert.DeserializeObject<T>(jsonstring);
  }

  public TimeConfig TimeConfig { get { return ReadConfig<TimeConfig>("time_config.json"); } }
  public PlayerConfig PlayerConfig { get { return ReadConfig<PlayerConfig>("player_config.json"); } }
  public BuildingConfig BuildingConfig { get { return ReadConfig<BuildingConfig>("building_config.json"); } }
  public CommodityConfig CommodityConfig { get { return ReadConfig<CommodityConfig>("commodity_config.json"); } }

  public override string ToString () {
    return JsonConvert.SerializeObject(this);
  }

}