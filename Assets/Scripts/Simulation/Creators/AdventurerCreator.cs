using UnityEngine;
using System.Collections;

public class AdventurerCreator {

  Simulation sim;

  public AdventurerCreator (Simulation _sim) {
    sim = _sim;
  }

  public Adventurer Create (string classKey) {
    var adventurer = new Adventurer(classKey);
    SetDefaults(adventurer);

    sim.player.Adventurers[adventurer.id] = adventurer;

    var data = new Hashtable();
    data["adventurer"] = adventurer;
    NotificationCenter.PostNotification(Constants.OnAdventurerCreated, data);

    return adventurer;
  }

  void SetDefaults (Adventurer adventurer) {
    adventurer.Name = Name.Generate();
    adventurer.Location = sim.map.TavernLocation;
    adventurer.Level = sim.config.adventurerDefaultLevel;
    adventurer.Experience = sim.config.adventurerDefaultExp;
    adventurer.Speed = sim.config.adventurerDefaultSpeed;
  }
}
