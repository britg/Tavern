using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {

  public float Gold { get; set; }
  public List<Building> Buildings { get; set; }
  public float Expenses { get; set; }
  public float BaseHourlyIncome { get; set; }
  public float Income { get; set; }
  public float NetIncome {
    get {
      return Income - Expenses;
    }
  }

  PlayerConfig config;
  public Player (PlayerConfig _config) {
    config = _config;
    Gold = config.start_gold;
    BaseHourlyIncome = config.base_hourly_income;
    Buildings = new List<Building>();
    SetupStartBuildings();
  }

  void SetupStartBuildings () {
    foreach (string buildingType in config.start_buildings) {
      CreateBuilding(buildingType);
    }
  }

  void CreateBuilding (string buildingType) {
    Building bldg = Building.FromTemplate(buildingType);
    bldg.CompleteNow();
    Buildings.Add(bldg);
  }

}
