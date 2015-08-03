using UnityEngine;
using System.Collections;

public class Trigger {

  public static string damageKey = "damage";
  public static string targetKey = "target";
  public static string statKey = "stat";
  public static string statChangeAmountKey = "statChangeAmount";
  public static string statChangeTargetKey = "statChangeTarget";

  public enum Type {
    NewFloor,
    PlayerStatChange
  }

  public Type type { get; set; }
  public Hashtable data = new Hashtable();

  public Trigger (Type _type) {
    type = _type;
  }

}
