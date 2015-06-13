using UnityEngine;
using System.Collections;

public class Building {

  public string Name { get; set; }
  public BuildingType Type { get; set; }

  public Building (string buildingKey) {
    Type = BuildingType.all[buildingKey];
  }
  
}
