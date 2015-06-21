using UnityEngine;
using System.Collections;

public class TriggerProcessor {

  Simulation sim;

  public TriggerProcessor (Simulation _sim) {
    sim = _sim;
  }

  public void Process (Trigger trigger) {
    if (trigger.type == Trigger.Type.NewFloor) {
      var floorStat = sim.player.Stats["tower_floor"];
      floorStat.Value += 1;
    }
  }
}
