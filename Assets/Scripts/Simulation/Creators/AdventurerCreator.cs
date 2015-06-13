using UnityEngine;
using System.Collections;

public class AdventurerCreator {

  Simulation sim;

  public AdventurerCreator (Simulation _sim) {
    sim = _sim;
  }

  public Adventurer Create (string classKey) {
    var adventurer = new Adventurer(classKey);
    sim.player.Adventurers[adventurer.id] = adventurer;
    return adventurer;
  }
}
