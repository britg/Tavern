using UnityEngine;
using System.Collections;

public class Trigger {

  public static string damageKey = "damage";
  public static string targetKey = "target";
  public static string statKey = "stat";

  public enum Type {
    NewFloor,
    StatChange
  }

  public Type type { get; set; }
  public Hashtable data;

  public Trigger (Type _type) {
    type = _type;
  }

}
