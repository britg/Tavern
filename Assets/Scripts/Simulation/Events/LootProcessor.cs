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
    var eq = ev.Equipment;
    SlotType slotType = eq.SlotType;
    Slot playerSlot = sim.player.Slots[slotType.Key];
    var prevEquipment = playerSlot.Equipment;
    playerSlot.Equipment = eq;
    ev.Equipment = prevEquipment;
    NotificationCenter.PostNotification(Constants.OnUpdateEvents);
    NotificationCenter.PostNotification(Constants.OnUpdateStats);
  }

}
