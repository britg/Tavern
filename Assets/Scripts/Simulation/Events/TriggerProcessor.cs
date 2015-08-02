using UnityEngine;
using System.Collections;

public class TriggerProcessor {

  Simulation sim;

  public TriggerProcessor (Simulation _sim) {
    sim = _sim;
  }

  public void Process (Trigger trigger) {
    if (trigger.type == Trigger.Type.NewFloor) {
      //TODO: update player's floor and all associatd
      //stuff
      sim.player.tower.floorNum += 1;
    }

    if (trigger.type == Trigger.Type.StatChange) {
      var damage = (float)trigger.data[Trigger.damageKey];
      var mob = (Mob)trigger.data[Trigger.targetKey];
      mob.ChangeStat(Stat.hp, damage);

      NotificationCenter.PostNotification(Constants.OnUpdateStats);
    }
  }
}
