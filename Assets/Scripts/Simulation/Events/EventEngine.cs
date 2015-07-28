﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class EventEngine {

  Simulation sim;
  Player player {
    get {
      return sim.player;
    }
  }

  public bool canContinue {
    get {
      if (player.lastEvent == null) {
        return true;
      }
      return player.lastEvent.conditionsSatisfied;
    }
  }

  public EventEngine (Simulation _sim) {
    sim = _sim;
  }

  public List<PlayerEvent> Continue () {

    List<PlayerEvent> newEvents = new List<PlayerEvent>();

    if (player.lastEvent == null) {
      newEvents = IntroSequence();
    } else if (player.lastEvent.chosenKey != null) {
      newEvents = ExecuteChoice(player.lastEvent);
    }

    newEvents.AddRange(EventsForPlayer());
    player.lastEvent = newEvents[newEvents.Count - 1];

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

    if (ev.type == PlayerEvent.Type.Equipment) {
      var lootProcessor = new LootProcessor(sim);
      lootProcessor.HandleAction(ev, actionName);
    } 

    NotificationCenter.PostNotification(Constants.OnUpdateEvents);
  }

  public void TriggerChoice (PlayerEvent ev, string choiceKey) {
    Debug.Log ("Trigger action " + choiceKey + " for event " + ev.Content);

    // TODO: Refactor into a choice processor when necessary
    ev.chosenKey = choiceKey;
    ev.conditionsSatisfied = true;

    NotificationCenter.PostNotification(Constants.OnUpdateEvents);
  }

  List<PlayerEvent> IntroSequence () {
    var list = new List<PlayerEvent>();
    var intro = sim.config.jsonCache["IntroEvents"][0];
    var eventsJson = intro["events"];
    foreach (JSONNode node in eventsJson.AsArray) {
      var eventStr = node.Value;
      list.Add(new PlayerEvent(eventStr));
    }

//    list.AddRange(Dev_RandomLoot());
    list.Add(Consider());

    return list;
  }

  List<PlayerEvent> Dev_RandomLoot () {
    var list = new List<PlayerEvent>();
    var eqGen = new EquipmentGenerator(sim);
    var rand = Random.Range(7, 15);
    var eRand = Random.Range(1, 4);
    for (int i = 0; i < 1; i++) {
      var eq = eqGen.Generate();
      var ev = PlayerEvent.Loot(eq);

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
    pullLeft.key = Constants.c_Shop;
    pullLeft.direction = Choice.Direction.Left;
    var pullRight = new Choice();
    pullRight.label = "Enter Tower";
    pullRight.key = Constants.c_Tower;
    pullRight.direction = Choice.Direction.Right;
    var msg = "You consider your next course of action...";
    return PlayerEvent.Choice(msg, pullLeft, pullRight);
  }

  List<PlayerEvent> ExecuteChoice (PlayerEvent ev) {
    List<PlayerEvent> newEvents = new List<PlayerEvent>();

    switch (ev.chosenKey) {
      case Constants.c_Tower:
        newEvents.AddRange(EnterTower());
        break;
      case Constants.c_Shop:
        newEvents.AddRange(EnterShop());
        break;
      default:
        Debug.Log("Unhandled choice " + ev.chosenKey);
        break;
    }

    return newEvents;
  }

  List<PlayerEvent> EnterTower () {
    var newEvents = new List<PlayerEvent>();
    newEvents.Add(new PlayerEvent("[Debug] Entering tower"));
    sim.player.location = Player.Location.Tower;

    NotificationCenter.PostNotification(Constants.OnUpdateStats);
    return newEvents;
  }

  List<PlayerEvent> EnterShop () {
    var newEvents = new List<PlayerEvent>();
    newEvents.Add(new PlayerEvent("[Debug] Entering store"));
    sim.player.location = Player.Location.Shop;
    NotificationCenter.PostNotification(Constants.OnUpdateStats);
    return newEvents;
  }

  List<PlayerEvent> EventsForPlayer () {
    var newEvents = new List<PlayerEvent>();

    if (sim.player.location == Player.Location.Shop) {
      // do store events
      var shopEventEngine = new ShopEventEngine(sim);
      newEvents.AddRange(shopEventEngine.Continue());
    } else if (sim.player.location == Player.Location.Tower) {
      var towerEventEngine = new TowerEventEngine(sim);
      newEvents.AddRange(towerEventEngine.Continue());
    }

    return newEvents;
  }

}
