using UnityEngine;
using System.Collections;

public class LootProcessor {

  Simulation sim;

  public LootProcessor (Simulation _sim) {
    sim = _sim;
  }

  public void PickUp (PlayerEvent ev) {
    Debug.Log ("Loot processor is picking up " + ev.Equipment.Name);
  }

  public void Equip (PlayerEvent ev) {
    Debug.Log ("Loot processor is equipping " + ev.Equipment.Name);
  }

}
