using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class EquipmentType {

  public const string type = "EquipmentType";

  public string Key { get; set; }
  public string Name { get; set; }
  public Dictionary<string, SlotType> SlotTypes { get; set; }
  public SlotType PrimarySlotType {
    get {
      return FirstSlotType();
    }
  }
  public EquipmentDesignation Designation {get; set;}

  public static Dictionary<string, EquipmentType> all = new Dictionary<string, EquipmentType>();

  public static void Cache (JSONNode json) {
    var equipmentType = new EquipmentType(json);
    all[equipmentType.Key] = equipmentType;
    Debug.Log("Loaded equipment type " + equipmentType.Name);
  }

  public EquipmentType () {

  }

  public EquipmentType (JSONNode json) {
    Key = json["key"].Value;
    Name = json["name"].Value;
    Designation = ParseDesignation(json["designation"].Value);

    SlotTypes = new Dictionary<string, SlotType>();
    var slotTypeArr = json["slots"].AsArray;
    
    foreach (JSONNode slotType in slotTypeArr) {
      var key = slotType.Value;
      SlotTypes.Add(key, SlotType.all[key]);
    }
  }

  public static EquipmentDesignation ParseDesignation (string desig) {
    var d = EquipmentDesignation.Weapon;
    switch (desig) {
    case "weapon":
      d = EquipmentDesignation.Weapon;
      break;
    case "armor":
      d = EquipmentDesignation.Armor;
      break;
    case "accessory":
      d = EquipmentDesignation.Accessory;
      break;
    }

    return d;
  }

  public SlotType FirstSlotType () {
    foreach (KeyValuePair<string, SlotType> p in SlotTypes) {
      return p.Value;
    }

    return null;
  }
}
