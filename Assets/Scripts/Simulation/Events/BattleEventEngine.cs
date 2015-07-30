using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleEventEngine {

  Simulation sim;

  Mob currentMob {
    get {
      return sim.player.tower.currentMob;
    }
  }

  public BattleEventEngine (Simulation _sim) {
    sim = _sim;
  }

  public List<PlayerEvent> StartBattle (Mob mob) {
    var newEvents = new List<PlayerEvent>();

    sim.player.tower.currentMob = mob;
    newEvents.Add(new PlayerEvent("Starting battle with mob " + mob.name));

    return newEvents;
  }

  public List<PlayerEvent> Continue () {
    var newEvents = new List<PlayerEvent>();

    newEvents.Add(new PlayerEvent("Continuing battle with mob " + currentMob.name));

    return newEvents;
  }

}
