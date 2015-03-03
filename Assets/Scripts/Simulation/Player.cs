using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {

  public int Gold { get; set; }
  public List<Building> Buildings { get; set; }
  public float Expenses { get; set; }
  public float Income { get; set; }

  PlayerConfig config;
  public Player (PlayerConfig _config) {
    config = _config;
    Gold = config.start_gold;
    Buildings = new List<Building>();
    SetupStartBuildings();
  }

  void SetupStartBuildings () {
    foreach (Building.Type buildingType in config.start_buildings) {
      CreateBuilding(buildingType);
    }
  }

  void CreateBuilding (Building.Type buildingType) {
    Building bldg = Building.FromTemplate(buildingType);
    bldg.CompleteNow();
    Buildings.Add(bldg);
  }

}
