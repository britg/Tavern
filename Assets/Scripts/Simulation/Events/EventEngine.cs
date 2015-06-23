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
    var eqGen = new EquipmentGenerator(sim);

    for (int i = 0; i < Random.Range(0, 5); i++) {
      var eq = eqGen.Generate();
      var ev = PlayerEvent.Loot(eq);
      var fill = new PlayerEvent("Killed [Giant Rat]");
      list.Add(ev);
      list.Add(fill);
    }

    return list;
  }

  public void TriggerEvent (PlayerEvent ev) {
    var triggerProcessor = new TriggerProcessor(sim);
    foreach (Trigger trigger in ev.Triggers) {
      triggerProcessor.Process(trigger);
      ev.hasTriggered = true;
      ev.Update();
    }
  }

  public void TriggerAction (PlayerEvent ev, string actionName) {
    Debug.Log ("Trigger action " + actionName + " for event " + ev.Content);
    var lootProcessor = new LootProcessor(sim);
    if (actionName == "pickUp") {
      lootProcessor.PickUp(ev);
    } else if (actionName == "equip") {
      lootProcessor.Equip(ev);
    }
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
