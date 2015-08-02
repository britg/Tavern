﻿using UnityEngine;
using System.Collections;

public class Stat {

  public static string hp = "hp";
  public static string ap = "ap";
  public static string dps = "dps";
  public static string def = "def";
  public static string spd = "spd";
  public static string luck = "luck";
  public static string res = "res";


  public string Key { get; set; }

  public StatType Type { get; set; }
  public float Min { get; set; }
  public float Max { get; set; }
  public float Base { get; set; }
  public float CurrentChange { get; set; }
  public float Value { get; set; }

  public string Abbr {
    get {
      return Type.Abbr;
    }
  }

  public Stat (string statKey) {
    Key = statKey;
    Type = StatType.all[statKey];
  }

  public Stat (string statKey, float min, float max) {
    Key = statKey;
    Type = StatType.all[statKey];
    Min = min;
    Max = max;
  }

  public Stat (string statKey, float value) {
    Key = statKey;
    Type = StatType.all[statKey];
    ChangeBase(value);
  }

  public void RollBase () {
    ChangeBase(Random.Range(Min, Max));
  }

  public void ChangeBase (float change) {
    Base += change;
    RecalcValue();
  }

  public void Change (float change) {
    CurrentChange += change;
    RecalcValue();
  }

  void RecalcValue () {
    Value = Base + CurrentChange;
  }


}
