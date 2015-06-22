using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipment {

  public EquipmentType Type { get; set; }
  public int Level { get; set; }
  public Rarity Rarity { get; set; }
  public string Name { get; set; }
  public SlotType SlotType { get; set; }

  public List<Stat> Stats = new List<Stat>();

}
