using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipment {

  public EquipmentType Type { get; set; }
  public int Level { get; set; }
  public Rarity Rarity { get; set; }
  public string Name { get; set; }
  public SlotType SlotType { get; set; }

  public Dictionary<string, Stat> Stats = new Dictionary<string, Stat>();


  public float StatValue (string key) {
    if (!Stats.ContainsKey(key)) {
      return 0f;
    }

    var stat = Stats[key];
    return stat.Value;
  }

}
