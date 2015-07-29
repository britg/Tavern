using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractibleEventEngine {

  Simulation sim;

  public InteractibleEventEngine (Simulation _sim) {
    sim = _sim;
  }

  public List<PlayerEvent> Continue () {
    var newEvents = new List<PlayerEvent>();
    return newEvents;
  }
}
