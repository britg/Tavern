using UnityEngine;
using System.Collections;

public class BuildingCreator {

  Simulation sim;

  public BuildingCreator (Simulation _sim) {
    sim = _sim;
  }

  public Building Create (string buildingKey) {
    var building = new Building(buildingKey);
    sim.player.Buildings[buildingKey] = building;

    
    return building;
  }
}
