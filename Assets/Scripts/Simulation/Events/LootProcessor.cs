using UnityEngine;
using System.Collections;

public class LootProcessor {

  Simulation sim;

  public LootProcessor (Simulation _sim) {
    sim = _sim;
  }

  public void HandleAction (PlayerEvent ev, string actionName) {
    if (actionName == Constants.c_Pickup) {
      ev.chosenKey = Constants.c_Pickup;
      PickUp(ev);
    } else if (actionName == Constants.c_Equip) {
      ev.chosenKey = Constants.c_Equip;
      Equip(ev);
    }
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
    NotificationCenter.PostNotification(Constants.OnUpdateStats);
  }

}
