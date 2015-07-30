using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionEventEngine {

  Simulation sim;

  public InteractionEventEngine (Simulation _sim) {
    sim = _sim;
  }

  public List<PlayerEvent> StartInteraction (Interactible interactible) {
    var newEvents = new List<PlayerEvent>();
    sim.player.tower.currentInteractible = interactible;

    newEvents.Add (PlayerEvent.Info ("[DEV] You find " + interactible.name + " and must make a choice about it."));

    // DEV
    newEvents.Add (PlayerEvent.Info ("[DEV] ending interaction...."));
    sim.player.tower.currentInteractible = null;

    return newEvents;
  }

  public List<PlayerEvent> Continue () {
    var newEvents = new List<PlayerEvent>();
    return newEvents;
  }
}
