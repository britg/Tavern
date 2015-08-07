using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipmentGenerator {

  Simulation sim;

  public EquipmentGenerator (Simulation _sim) {
    sim = _sim;
  }

  public Equipment Generate () {
    List<string> keyList = new List<string>(EquipmentType.all.Keys);
    var randInt = Random.Range(0, keyList.Count - 1);
    var eqTypeKey = keyList[randInt];
    return Generate(eqTypeKey);
  }

  public Equipment Generate (string eqTypeKey) {
    var eqType = EquipmentType.all[eqTypeKey];
    return Generate(eqType);
  }

  public Equipment Generate (EquipmentType type) {
    var e = new Equipment();
    e.Type = type;
    e.SlotType = type.PrimarySlotType;
    e.Name = type.Name;
    var rarityGen = new RarityGenerator(sim);
    e.Rarity = rarityGen.Roll();
    AssignAttributes(e);

    return e;
  }

  void AssignAttributes (Equipment e) {

    // Get base stat from equipment designation
    foreach (KeyValuePair<string, StatType> pair in e.Designation.BaseStats) {
      var statType = pair.Value;
      e.Stats[statType.Key] = new Stat(statType.Key, Random.Range (1f, 5f));
    }


//    var stat = new Stat(Stat.dps, 1f);
//    e.Stats[stat.Type.Key] = stat;
  }

}
