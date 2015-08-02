using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCombatProcessor  {

  public const string basicAttack = "basic";
  public const string specialAttack = "special";

  Player player;
  Mob mob;

  public PlayerCombatProcessor (Player _player, Mob _mob) {
    player = _player;
    mob = _mob;
  }

  public List<PlayerEvent>  TakeAction () {
    var newEvents = new List<PlayerEvent>();

    // Default action is to take a swing
    var chances = iTween.Hash(
      basicAttack, 75f/*,
      specialAttack, 25
      */
    );

    var chosen = Roll.Hash(chances);

    if (chosen == basicAttack) {
      newEvents.AddRange(BasicAttack());
    }

    return newEvents;
  }

  List<PlayerEvent> BasicAttack () {

    var newEvents = new List<PlayerEvent>();

    // Calc damage
    // TODO: Miss chance?
    // TODO: Crit chance
    // TODO: Adjust value up and down for def
    var damage = player.GetStatValue(Stat.dps);
    mob.ChangeStat(Stat.hp, -damage);

    var ev = new PlayerEvent();
    ev.type = PlayerEvent.Type.PlayerBasicAttack;
    ev.data[PlayerEvent.mobKey] = mob;
    ev.data[PlayerEvent.damageKey] = damage;

    newEvents.Add(ev);
    return newEvents;
  }

}
