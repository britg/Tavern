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
    BootstrapAdventurers();
    BootstrapBuildings();
  }

  void BootstrapResources () {
    var resource = new Resource("gold", sim.config.initialGold);
    player.Resources["gold"] = resource;
  }

  void BootstrapAdventurers () {
    var adventurerCreator = new AdventurerCreator(sim);
    adventurerCreator.Create("warrior");
    adventurerCreator.Create("warrior");
  }

  void BootstrapBuildings () {
    foreach (string buildingKey in sim.config.startBuildings) {
      CreateBuilding(buildingKey);
    }
  }

  void CreateBuilding (string buildingKey) {
  }

}
