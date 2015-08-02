using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleProcessor {

  Simulation sim;
  int iterationCount = 0;
  int iterationLimit = 20;

  Mob currentMob {
    get {
      return sim.player.tower.currentMob;
    }
  }

  public BattleProcessor (Simulation _sim) {
    sim = _sim;
  }

  public List<PlayerEvent> StartBattle (Mob mob) {
    var newEvents = new List<PlayerEvent>();

    sim.player.tower.currentMob = mob;
    sim.player.currentInitiative = 0f;
    mob.currentInitiative = 0f;
    newEvents.Add(new PlayerEvent("! [" + mob.name + "]"));
    newEvents.AddRange(Continue());

    return newEvents;
  }

  public List<PlayerEvent> Continue () {
    var newEvents = new List<PlayerEvent>();

    if (!PlayerAlive()) {
      // hand player dead situation
    }

    if (!MobAlive()) {
      newEvents.AddRange(Victory());
      return newEvents;
    }

    var initiativeProcessor = new InitiativeProcessor(sim.player, sim.player.tower.currentMob);
    string nextMove = initiativeProcessor.NextMove();

    if (nextMove == InitiativeProcessor.playerIdent) {
      var playerCombatProcessor = new PlayerCombatProcessor(sim.player, sim.player.tower.currentMob);
      var events = playerCombatProcessor.TakeAction();
      newEvents.AddRange(events);
    } else {
      var mobCombatProcessor = new MobCombatProcessor(sim.player, sim.player.tower.currentMob);
      var events = mobCombatProcessor.TakeAction();
      newEvents.AddRange(events);
    }

    if (iterationCount >= iterationLimit) {
      newEvents.Add(PlayerEvent.Info("[DEV] Iteration limit reached! Something went wrong."));
      return newEvents;
    }

    if (newEvents.Count > 0 && newEvents[newEvents.Count - 1].hasChoices) {
      return newEvents;
    } else {
      Debug.Log("Iterating battle processor continue");
      ++iterationCount;
      newEvents.AddRange(Continue());
      return newEvents;
    }
  }

  bool PlayerAlive () {
    var hp = sim.player.GetStat(Stat.hp);
    return hp.Value > 0f;
  }

  bool MobAlive () {
    Mob mob = sim.player.tower.currentMob;
    var hp = mob.GetStatValue(Stat.hp);
    Debug.Log("mob's hp is " + hp);
    return hp > 0f;
  }

  public List<PlayerEvent> Victory () {

    var newEvents = new List<PlayerEvent>();

    newEvents.Add(PlayerEvent.Info("Victory!"));
    newEvents.Add(PlayerEvent.Info("Experience gain"));

    newEvents.Add(PlayerEvent.Info("Loot"));
    newEvents.Add(PlayerEvent.Info("Loot"));

//    newEvents.AddRange(AfterBattleChoices());

    sim.player.tower.currentMob = null;
    return newEvents;
  }


  public List<PlayerEvent> AfterBattleChoices () {
    var pullLeft = Choice.PullLeft(Choice.Potion, "Drink Potion");
    var pullRight = Choice.PullRight(Choice.Continue, "Continue");
    var msg = "You catch you breath after battle...";
    return new List<PlayerEvent>(){ PlayerEvent.Choice(msg, pullLeft, pullRight) };
  }

}
