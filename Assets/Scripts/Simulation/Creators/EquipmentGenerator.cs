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
    e.Name = type.Name;

    foreach (string k in type.SlotTypes.Keys) {
      e.SlotType = type.SlotTypes[k];
      break;
    }

    return e;
  }

}
