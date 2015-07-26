using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class EventEngine {

  Simulation sim;

  bool hasSeenIntro = false;
  PlayerEvent lastEvent;

  public bool canContinue {
    get {
      if (lastEvent == null) {
        return true;
      }
      return lastEvent.conditionsSatisfied;
    }
  }

  public EventEngine (Simulation _sim) {
    sim = _sim;
  }

  public List<PlayerEvent> Continue () {

    List<PlayerEvent> newEvents;

    if (!hasSeenIntro) {
      hasSeenIntro = true;
      newEvents = IntroSequence();
    } else {
      newEvents = Dev_RandomLoot();
    }

    lastEvent = newEvents[newEvents.Count - 1];

    return newEvents;
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
    } else {

      // TODO actually parse the action name and do something
      ev.conditionsSatisfied = true;
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

    list.Add(Consider());

    return list;
  }

  List<PlayerEvent> Dev_RandomLoot () {
    var list = new List<PlayerEvent>();
    var eqGen = new EquipmentGenerator(sim);
    var rand = Random.Range(7, 15);
    var eRand = Random.Range(1, 4);
    for (int i = 0; i < rand; i++) {
      var eq = eqGen.Generate();
      var ev = PlayerEvent.Loot(eq);
      var fill = new PlayerEvent("Killed [Giant Rat]");

      for (int j = 0; j < eRand; j++) {
        //        list.Add(fill);
      }
      list.Add(ev);
    }

    return list;
  }

  PlayerEvent Consider () {
    var pullLeft = new Choice();
    pullLeft.label = "Shop";
    pullLeft.key = "shop";
    pullLeft.direction = Choice.Direction.Left;
    var pullRight = new Choice();
    pullRight.label = "Enter Tower";
    pullRight.key = "tower";
    pullRight.direction = Choice.Direction.Right;
    var msg = "You consider your next course of action...";
    return PlayerEvent.Choice(msg, pullLeft, pullRight);
  }

}
