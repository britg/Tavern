using UnityEngine;
using System.Collections;

public class EquipmentGenerator {

  Simulation sim;

  public EquipmentGenerator (Simulation _sim) {
    sim = _sim;
  }

  public Equipment Generate (string eqTypeKey) {
    var eqType = EquipmentType.all[eqTypeKey];
    return Generate(eqType);
  }

  public Equipment Generate (EquipmentType type) {
    var e = new Equipment();
    e.Type = type;
    e.Name = type.Name;

    return e;
  }

}
