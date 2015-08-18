using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class InputProcessor {

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

  public InputProcessor (Simulation _sim) {
    sim = _sim;
  }

  public List<PlayerEvent> Continue () {

    List<PlayerEvent> newEvents = new List<PlayerEvent>();

    if (player.lastEvent == null) {
      NotificationCenter.PostNotification(Constants.OnFirstPull);
      EnterTower();
      return IntroSequence();
//      return Dev_StraightToTower();
//      return Dev_RandomLoot();
    }

    //if (player.lastEvent.chosenKey == Choice.Tower) {
    //  newEvents.AddRange(EnterTower());
    //}

    //if (player.lastEvent.chosenKey == Choice.Shop) {
    //  newEvents.AddRange(EnterShop());
    //} 

    //if (sim.player.location == Player.Location.Shop) {
    //  var shopProcessor = new ShopProcessor(sim);
    //  newEvents.AddRange(shopProcessor.Continue());
    //  // DEV
    //  newEvents.Add(Consider());
    //}

    if (sim.player.location == Player.Location.Tower) {
      var towerProcessor = new TowerProcessor(sim);
      newEvents.AddRange(towerProcessor.Continue());
    }

    if (newEvents.Count > 0) {
      player.lastEvent = newEvents[newEvents.Count - 1];
    }

    return newEvents;
  }

  public void TriggerEvent (PlayerEvent ev) {
    foreach (Trigger trigger in ev.Triggers) {
      var triggerProcessor = new TriggerProcessor(sim, trigger);
      triggerProcessor.Process();
      ev.hasTriggered = true;
      ev.Update();
    }
  }

  public void TriggerAction (PlayerEvent ev, string actionName) {
    Debug.Log ("Trigger action " + actionName + " for event " + ev.Content);

    if (ev.type == PlayerEvent.Type.Equipment) {
      var equipmentActionProcessor = new EquipmentActionProcessor(sim);
      equipmentActionProcessor.HandleAction(ev, actionName);
    }

    if (ev.type == PlayerEvent.Type.Consumable) {
      var consumableActionProcessor = new ConsumableActionProcessor(sim);
      consumableActionProcessor.HandleAction(ev, actionName);
    }

    NotificationCenter.PostNotification(Constants.OnUpdateEvents);
  }

  public void TriggerChoice (PlayerEvent ev, string choiceKey) {
    Debug.Log ("Trigger choice " + choiceKey + " for event " + ev.Content);

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

      if (eventStr == "battle") {
        var towerProcessor = new TowerProcessor(sim);
        list.AddRange(towerProcessor.Continue());
      } else {
        list.Add(PlayerEvent.Story(eventStr));
      }
    }

    //list.AddRange(Dev_RandomLoot());
//    list.Add(Consider());

    player.lastEvent = list[list.Count - 1];

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

      for (int j = 0; j < eRand; j++) {
        //        list.Add(fill);
      }
      list.Add(ev);
    }

    return list;
  }

  List<PlayerEvent> Dev_StraightToTower () {
    var ev = PlayerEvent.Info("[DEV] entering tower...");
    ev.chosenKey = "tower";
    player.lastEvent = ev;
    return new List<PlayerEvent>(){ ev };
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



    return newEvents;
  }

  List<PlayerEvent> EnterTower () {
    var newEvents = new List<PlayerEvent>();
    sim.player.location = Player.Location.Tower;

    NotificationCenter.PostNotification(Constants.OnUpdateStats);
    return newEvents;
  }

  List<PlayerEvent> EnterShop () {
    var newEvents = new List<PlayerEvent>();
    sim.player.location = Player.Location.Shop;
    NotificationCenter.PostNotification(Constants.OnUpdateStats);
    return newEvents;
  }

}
