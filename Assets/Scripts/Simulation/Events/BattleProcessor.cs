using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleProcessor {

  Simulation sim;

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
//    newEvents.Add(new PlayerEvent("Starting battle with mob " + mob.name));

    newEvents.AddRange(Continue ());

    return newEvents;
  }

  public List<PlayerEvent> Continue () {
    var newEvents = new List<PlayerEvent>();

    if (!PlayerAlive()) {
      // hand player dead situation
    }

    if (!MobAlive()) {
      return Victory();
    }


//    newEvents.Add(PlayerEvent.Info("Continuing battle with mob " + currentMob.name));
    /*
     * while (palyerAlive && mobAlive && !lastEvent.hasChoice)
     *    currentMove = initiativeProcessor.NextMove()
     *    if (playerMove)
     *        newEvents.AddRange(playerCombatProcessor(player, mob).TakeAction())
     *    else
     *        newEvents.AddRange(mobCombatProcessor(player, mob).TakeAction())
     */

    newEvents.Add(PlayerEvent.Info("Deal damage"));
    newEvents.Add(PlayerEvent.Info("Deal damage"));
    newEvents.Add(PlayerEvent.Info("Take damage"));
    newEvents.Add(PlayerEvent.Info("Deal damage"));

    newEvents.AddRange(Victory());

    return newEvents;
  }

  bool PlayerAlive () {
    var hp = sim.player.GetStat("hp");
    return hp.Value > 0f;
  }

  bool MobAlive () {
    Mob mob = sim.player.tower.currentMob;
    var hp = mob.GetStat("hp");
    return hp.Value > 0f;
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
