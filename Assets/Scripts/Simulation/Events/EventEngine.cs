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

    for (int i = 0; i < 1; i++) {
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

    var eqMsgs = intro["equipmentMessages"].AsArray;
    foreach (JSONNode node in eqMsgs) {
      var eventStr = node.Value;
      list.Add(new PlayerEvent(eventStr));
    }

    var equipmentTypes = intro["equipmentTypes"].AsArray;

    var eqGen = new EquipmentGenerator(sim);
    foreach (JSONNode node in equipmentTypes) {
      var eqTypeKey = node.Value;
      var eq = eqGen.Generate(eqTypeKey);
      var e = PlayerEvent.Loot(eq);
      list.Add(e);
    }

    var exitMsgs = intro["exitMessages"].AsArray;
    foreach (JSONNode node in exitMsgs) {
      var eventStr = node.Value;
      list.Add(new PlayerEvent(eventStr));
    }

    transitionJson = intro["levelTransition"];
    transitionName = transitionJson.Value;
    var floorChange = PlayerEvent.Transition(transitionName);
    floorChange.Triggers.Add(new Trigger(Trigger.Type.NewFloor));
    list.Add(floorChange);


    return list;
  }
}
