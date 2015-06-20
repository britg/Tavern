using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventEngine {

  Simulation sim;

  public EventEngine (Simulation _sim) {
    sim = _sim;
  }

  public List<PlayerEvent> Input () {
    var list = new List<PlayerEvent>();

    for (int i = 0; i < 10; i++) {
      list.Add(new PlayerEvent("hello"));
    }

    return list;
  }

}
