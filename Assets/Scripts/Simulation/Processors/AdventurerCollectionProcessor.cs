using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdventurerCollectionProcessor : Processor {

  public AdventurerCollectionProcessor (Simulation _sim) {
    sim = _sim;
  }

  public override void Start (Simulation _sim) {
    foreach (KeyValuePair<string, Adventurer> adv in sim.player.Adventurers) {
      var proc = new AdventurerProcessor(adv.Value, sim);
      proc.Start(_sim);
    }
    base.Start(_sim);
  }

  public override void Update (float deltaTime) {
    foreach (KeyValuePair<string, Adventurer> adv in sim.player.Adventurers) {
      var proc = new AdventurerProcessor(adv.Value, sim);
      proc.Update(deltaTime);
    }
    base.Update(deltaTime);
  }

  public override void OnMinute () {
    foreach (KeyValuePair<string, Adventurer> adv in sim.player.Adventurers) {
      var proc = new AdventurerProcessor(adv.Value, sim);
      proc.OnMinute();
    }
    base.OnMinute();
  }
}
