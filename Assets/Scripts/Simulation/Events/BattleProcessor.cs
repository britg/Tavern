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
    sim.player.tower.lastBattleMove = null;
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
      newEvents.AddRange(MobDeath());
      newEvents.AddRange(Victory());
      return newEvents;
    }

    var initiativeProcessor = new InitiativeProcessor(sim.player, currentMob);
    string nextMove = initiativeProcessor.NextMove();

    if (nextMove == InitiativeProcessor.playerIdent) {
      var playerCombatProcessor = new PlayerCombatProcessor(sim.player, currentMob);
      var events = playerCombatProcessor.TakeAction();
      newEvents.AddRange(events);
    } else {
      var mobCombatProcessor = new MobCombatProcessor(sim.player, currentMob);
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
    var hp = currentMob.GetStatValue(Stat.hp);
    Debug.Log("mob's hp is " + hp);
    return hp > 0f;
  }

  public List<PlayerEvent> MobDeath () {
    var newEvents = new List<PlayerEvent>();
    newEvents.Add (PlayerEvent.Info(currentMob.name + " dies!"));
    return newEvents;
  }

  public List<PlayerEvent> Victory () {

    var newEvents = new List<PlayerEvent>();

    newEvents.AddRange(GainExperience());
    newEvents.AddRange(Gold());
    newEvents.AddRange(Equipment());
    newEvents.AddRange(Consumables());

//    newEvents.AddRange(AfterBattleChoices());

    sim.player.tower.currentMob = null;
    return newEvents;
  }

  public List<PlayerEvent> Gold () {
    var newEvents = new List<PlayerEvent>();
    // Gold
    if (Roll.Percent(currentMob.goldChance)) {
      var goldGenerator = new GoldGenerator(sim.player, currentMob);
      var amount = goldGenerator.Mob();
      var ev = PlayerEvent.Info(string.Format("{0} gold", amount));
      var trigger = new Trigger(Trigger.Type.PlayerResourceChange);
      trigger.data[Trigger.resourceKey] = Resource.Gold;
      trigger.data[Trigger.resourceAmountKey] = amount;

      ev.Triggers.Add(trigger);
      newEvents.Add(ev);
    }

    return newEvents;
  }

  public List<PlayerEvent> Equipment () {
    var newEvents = new List<PlayerEvent>();

    // Chance for loot
    if (Roll.Percent(currentMob.lootChance)) {
      var equipmentGenerator = new EquipmentGenerator(sim);
      var eq = equipmentGenerator.Generate();
      newEvents.Add(PlayerEvent.Loot(eq));
    }

    return newEvents;
  }

  public List<PlayerEvent> Consumables () {
    var newEvents = new List<PlayerEvent>();
    // Chance for consumable
    if (Roll.Percent(currentMob.consumableChance)) {
      newEvents.Add(PlayerEvent.Info("Consumable"));
    }
    return newEvents;
  }

  public List<PlayerEvent> GainExperience () {
    var experienceProcessor = new ExperienceProcessor(sim.player, currentMob);
    var amount = experienceProcessor.ExperienceGain();
    var ev = PlayerEvent.Info(string.Format("+{0} experience", amount));
    var trigger = new Trigger(Trigger.Type.PlayerStatChange);
    trigger.data[Trigger.statKey] = Stat.xp;
    trigger.data[Trigger.statChangeAmountKey] = amount;
    ev.Triggers.Add(trigger);

    return new List<PlayerEvent>(){ ev };
  }

  public List<PlayerEvent> AfterBattleChoices () {
    var pullLeft = Choice.PullLeft(Choice.Potion, "Drink Potion");
    var pullRight = Choice.PullRight(Choice.Continue, "Continue");
    var msg = "You catch you breath after battle...";
    return new List<PlayerEvent>(){ PlayerEvent.Choice(msg, pullLeft, pullRight) };
  }

}
