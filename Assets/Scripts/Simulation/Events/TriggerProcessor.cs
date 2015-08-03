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

    if (trigger.type == Trigger.Type.PlayerStatChange) {
      var statKey = (string)trigger.data[Trigger.statKey];
      var amount = (float)trigger.data[Trigger.statChangeAmountKey];

      sim.player.ChangeStat(statKey, amount);

      if (statKey == Stat.hp && amount < 0) {
        NotificationCenter.PostNotification(Constants.OnTakeDamage);
      }

      NotificationCenter.PostNotification(Constants.OnUpdateStats);
    }
  }
}
