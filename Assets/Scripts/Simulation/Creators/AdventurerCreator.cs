using UnityEngine;
using System.Collections;

public class AdventurerCreator {

  Simulation sim;

  public AdventurerCreator (Simulation _sim) {
    sim = _sim;
  }

  public Adventurer Create (string classKey) {
    var adventurer = new Adventurer(classKey);
    adventurer.Name = Name.Generate();
    sim.player.Adventurers[adventurer.id] = adventurer;
    adventurer.Location = sim.map.TavernLocation;
    NotificationCenter.PostNotification(Constants.OnAdventurerCreated);
    return adventurer;
  }
}
