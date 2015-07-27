using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class EventEngine {

  Simulation sim;

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

    List<PlayerEvent> newEvents = new List<PlayerEvent>();

    if (lastEvent == null) {
      newEvents = IntroSequence();
    } else if (lastEvent.chosenKey != null) {
      newEvents = ExecuteChoice(lastEvent.chosenKey);
    }

    newEvents.AddRange(EventsForPlayer());
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

    if (ev.type == PlayerEvent.Type.Equipment) {
      HandleEquipmentTrigger(ev, actionName);
    } else {
      ev.chosenKey = actionName;
      ev.conditionsSatisfied = true;
    }

    NotificationCenter.PostNotification(Constants.OnUpdateEvents);
  }

  void HandleEquipmentTrigger (PlayerEvent ev, string actionName) {
    var lootProcessor = new LootProcessor(sim);
    if (actionName == Constants.c_Pickup) {
      ev.chosenKey = Constants.c_Pickup;
      lootProcessor.PickUp(ev);
    } else if (actionName == Constants.c_Equip) {
      ev.chosenKey = Constants.c_Equip;
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

    list.AddRange(Dev_RandomLoot());
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
//      var fill = new PlayerEvent("Killed [Giant Rat]");

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

  List<PlayerEvent> ExecuteChoice (string choiceKey) {
    List<PlayerEvent> newEvents = new List<PlayerEvent>();

    switch (choiceKey) {
      case Constants.c_Tower:
        newEvents.AddRange(EnterTower());
        break;
      case Constants.c_Shop:
        newEvents.AddRange(EnterShop());
        break;
      default:
        Debug.Log("Unhandled choice " + choiceKey);
        break;
    }

    return newEvents;
  }

  List<PlayerEvent> EnterTower () {
    var newEvents = new List<PlayerEvent>();
    newEvents.Add(new PlayerEvent("[Debug] Entering tower"));
    sim.player.location = Player.Location.Tower;
    return newEvents;
  }

  List<PlayerEvent> EnterShop () {
    var newEvents = new List<PlayerEvent>();
    newEvents.Add(new PlayerEvent("[Debug] Entering store"));
    sim.player.location = Player.Location.Shop;
    return newEvents;
  }

  List<PlayerEvent> EventsForPlayer () {
    var newEvents = new List<PlayerEvent>();

    if (sim.player.location == Player.Location.Shop) {
      // do store events
      var shopEventEngine = new ShopEventEngine(sim);
      newEvents.AddRange(shopEventEngine.Events());
    } else if (sim.player.location == Player.Location.Tower) {
      var towerEventEngine = new TowerEventEngine(sim);
      newEvents.AddRange(towerEventEngine.Events());
    }

    return newEvents;
  }

}
