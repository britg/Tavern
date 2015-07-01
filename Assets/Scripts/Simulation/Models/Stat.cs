using UnityEngine;
using System.Collections;

public class Stat {

  public StatType Type { get; set; }
  public float MaxValue { get; set; }
  public float Value { get; set; }
  public string Abbr {
    get {
      return Type.Abbr;
    }
  }

  public Stat (string statKey, float value) {
    Debug.Log ("Attempting to instantiate stat of type " + statKey);
    Type = StatType.all[statKey];
    Value = value;
    MaxValue = value;
  }

}
