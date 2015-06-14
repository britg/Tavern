using UnityEngine;
using System.Collections;

public class PlayerCreator {

  Simulation sim;
  Player player;

  public PlayerCreator (Simulation _sim) {
    sim = _sim;
  }

  public Player Create () {
    player = new Player();
    sim.player = player;
    Bootstrap();
    return player;
  }

  public void Bootstrap () {
    BootstrapResources();
    BootstrapBuildings();
    BootstrapAdventurers();
  }

  void BootstrapResources () {
    var resource = new Resource("gold", sim.config.initialGold);
    player.Resources["gold"] = resource;
  }

  void BootstrapAdventurers () {
    var adventurerCreator = new AdventurerCreator(sim);
    for (int i = 0; i < 2; i++) {
      adventurerCreator.Create("warrior");
    }
  }

  void BootstrapBuildings () {
    foreach (string buildingKey in sim.config.startBuildings) {
      CreateBuilding(buildingKey);
    }

    // Player always starts with a tavern
    Hashtable data = new Hashtable();
    data["tavern"] = sim.player.Buildings["tavern"];
    NotificationCenter.PostNotification(Constants.OnTavernCreated, data);
  }

  void CreateBuilding (string buildingKey) {
    var buildingCreator = new BuildingCreator(sim);
    buildingCreator.Create(buildingKey);
  }

}
