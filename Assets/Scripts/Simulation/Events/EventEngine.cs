using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class EventEngine {

  Simulation sim;

  public EventEngine (Simulation _sim) {
    sim = _sim;
  }

  public List<PlayerEvent> Input () {
    if (sim.player.Stats["tower_floor"].Value == 0f) {
      return IntroSequence();
    }

    var list = new List<PlayerEvent>();

    for (int i = 0; i < 10; i++) {
      list.Add(new PlayerEvent("hello"));
    }

    return list;
  }

  List<PlayerEvent> IntroSequence () {
    var list = new List<PlayerEvent>();
    var intro = sim.config.jsonCache["IntroEvents"][0];
    var eventsJson = intro["events"];
    foreach (JSONNode node in eventsJson.AsArray) {
      var eventStr = node.Value;
      list.Add(new PlayerEvent(eventStr));
    }

    var transitionJson = intro["transition"];
    var transitionName = transitionJson.Value;
    list.Add(PlayerEvent.Transition(transitionName));


    return list;
  }
}
