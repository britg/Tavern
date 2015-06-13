using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdventurerCollectionProcessor : Processor {

  Simulation sim;

  public AdventurerCollectionProcessor (Simulation _sim) {
    sim = _sim;
  }

  public override void OnMinute () {
    foreach (KeyValuePair<string, Adventurer> adv in sim.player.Adventurers) {
      var proc = new AdventurerProcessor(adv.Value, sim);
      proc.OnMinute();
    }
    base.OnMinute();
  }
}
