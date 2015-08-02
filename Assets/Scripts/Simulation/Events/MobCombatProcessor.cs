using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobCombatProcessor {

  public const string basicAttack = "basic";

  Player player;
  Mob mob;

  public MobCombatProcessor (Player _player, Mob _mob) {
    player = _player;
    mob = _mob;
  }

  public List<PlayerEvent> TakeAction () {
    var newEvents = new List<PlayerEvent>();
    var action = Roll.Hash(mob.combatProfile);

    if (action == basicAttack) {

    }
    
    newEvents.Add(PlayerEvent.Info("Mob taking action"));
    return newEvents;
  }
}
