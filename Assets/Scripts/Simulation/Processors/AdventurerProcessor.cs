using UnityEngine;
using System.Collections;

public class AdventurerProcessor : Processor {

  Adventurer adv;
  Simulation sim;

  public AdventurerProcessor (Adventurer _adv, Simulation _sim) {
    adv = _adv;
    sim = _sim;
  }

  public override void OnMinute() {
    Debug.Log(adv.ToString());
    base.OnMinute();
  }

}
