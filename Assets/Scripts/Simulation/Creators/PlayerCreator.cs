using UnityEngine;
using System.Collections;

public class PlayerCreator {

  SimulationConfig config;
  Player player;

  public PlayerCreator (SimulationConfig _config) {
    config = _config;
  }

  public Player Create () {
    player = new Player();

    var resource = new Resource("gold", config.initialGold);
    player.Resources["gold"] = resource;

    var adventurer = new Adventurer("warrior");
    player.Adventurers[adventurer.id] = adventurer;

    BootstrapBuildings();

    return player;
  }

  void BootstrapBuildings () {
    foreach (string buildingKey in config.startBuildings) {
      CreateBuilding(buildingKey);
    }
  }

  void CreateBuilding (string buildingKey) {
  }

}
