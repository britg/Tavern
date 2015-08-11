using UnityEngine;
using System.Collections;

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

    var consumable = (Consumable)ev.data[PlayerEvent.consumableKey];
    // Only action right now is to drink
    Debug.Log("Drinking potion");


  }
}
