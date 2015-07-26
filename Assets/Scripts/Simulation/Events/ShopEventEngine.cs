using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopEventEngine {

  Simulation sim;

  public ShopEventEngine (Simulation _sim) {
    sim = _sim;
  }

  public List<PlayerEvent> Events () {
    var newEvents = new List<PlayerEvent>();
    newEvents.Add(new PlayerEvent("shop event engine"));
    return newEvents;
  }

}
