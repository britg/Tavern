using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerEventEngine {

  Simulation sim;

  public TowerEventEngine (Simulation _sim) {
    sim = _sim;
  }

  public List<PlayerEvent> Events () {
    var newEvents = new List<PlayerEvent>();
    newEvents.Add(new PlayerEvent("tower event engine"));
    return newEvents;
  }

}
