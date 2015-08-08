using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipmentActionProcessor {

  Simulation sim;

  public EquipmentActionProcessor (Simulation _sim) {
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

    SubtractPrevEquipment(prevEquipment);
    AddNewEquipment(eq);

    NotificationCenter.PostNotification(Constants.OnUpdateStats);
  }

  void SubtractPrevEquipment (Equipment prev) {
    if (prev == null) {
      return;
    }

    foreach (KeyValuePair<string, Stat> pair in prev.Stats) {
      var stat = pair.Value;
      var playerStat = sim.player.GetStat(stat.Key);
      playerStat.Change(-stat.Value);
    }
  }

  void AddNewEquipment (Equipment eq) {
    foreach (KeyValuePair<string, Stat> pair in eq.Stats) {
      var stat = pair.Value;
      var playerStat = sim.player.GetStat(stat.Key);
      playerStat.Change(stat.Value);
    }
  }
}
