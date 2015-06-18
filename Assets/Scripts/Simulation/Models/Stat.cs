using UnityEngine;
using System.Collections;

public class Stat {

  public StatType Type { get; set; }
  public int Value { get; set; }

  public Stat (string statKey, int value) {
    Type = StatType.all[statKey];
    Value = value;
  }

}
