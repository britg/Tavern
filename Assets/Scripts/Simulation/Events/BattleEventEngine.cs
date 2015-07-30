using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleEventEngine {

  Simulation sim;

  Mob currentMob {
    get {
      return sim.player.tower.currentMob;
    }
  }

  public BattleEventEngine (Simulation _sim) {
    sim = _sim;
  }

  public List<PlayerEvent> StartBattle (Mob mob) {
    var newEvents = new List<PlayerEvent>();

    sim.player.tower.currentMob = mob;
    newEvents.Add(new PlayerEvent("Starting battle with mob " + mob.name));

    newEvents.AddRange(Continue ());

    return newEvents;
  }

  public List<PlayerEvent> Continue () {
    var newEvents = new List<PlayerEvent>();

    newEvents.Add(PlayerEvent.Info("Continuing battle with mob " + currentMob.name));

    newEvents.Add(PlayerEvent.Info("Deal damage"));
    newEvents.Add(PlayerEvent.Info("Deal damage"));
    newEvents.Add(PlayerEvent.Info("Take damage"));
    newEvents.Add(PlayerEvent.Info("Deal damage"));

    newEvents.AddRange(Victory());

    return newEvents;
  }

  public List<PlayerEvent> Victory () {

    var newEvents = new List<PlayerEvent>();

    newEvents.Add(PlayerEvent.Info("Victory!"));
    newEvents.Add(PlayerEvent.Info("Experience gain"));
    sim.player.tower.currentMob = null;

    newEvents.Add(PlayerEvent.Info("Loot"));
    newEvents.Add(PlayerEvent.Info("Loot"));

//    newEvents.AddRange(AfterBattleChoices());

    return newEvents;
  }


  public List<PlayerEvent> AfterBattleChoices () {
    var pullLeft = Choice.PullLeft(Choice.Potion, "Drink Potion");
    var pullRight = Choice.PullRight(Choice.Continue, "Continue");
    var msg = "You catch you breath after battle...";
    return new List<PlayerEvent>(){ PlayerEvent.Choice(msg, pullLeft, pullRight) };
  }

}
