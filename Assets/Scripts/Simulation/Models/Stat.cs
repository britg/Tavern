using UnityEngine;
using System.Collections;

public class Stat {

  public StatType Type { get; set; }
  public float Value { get; set; }

  public Stat (string statKey, float value) {
    Type = StatType.all[statKey];
    Value = value;
  }

}
