using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConsumableActionProcessor {

  Simulation sim;
  PlayerEvent ev;
  string actionKey;

  Equipment _eq;
  Equipment eq {
    get {
      if (_eq == null) {
        _eq = (Equipment)ev.data[PlayerEvent.equipmentKey];
      }
      return _eq;
    }
  }

  public ConsumableActionProcessor (Simulation _sim) {
    sim = _sim;
  }

  public void HandleAction (PlayerEvent _ev, string _actionKey) {
    ev = _ev;
    actionKey = _actionKey;
    ev.chosenKey = actionKey;

    var consumable = (Consumable)ev.data[PlayerEvent.consumableKey];
    // Only action right now is to drink
    foreach (KeyValuePair<string, float> statEffect in consumable.statEffects) {
      sim.player.ChangeStat(statEffect.Key, statEffect.Value);
    }

    ev.Content = "Empty Flask";

    NotificationCenter.PostNotification(Constants.OnUpdateStats);
    NotificationCenter.PostNotification(Constants.OnUpdateEvents);

  }
}
