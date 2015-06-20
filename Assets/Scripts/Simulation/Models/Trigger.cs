using UnityEngine;
using System.Collections;

public class Trigger {

  public enum Type {
    NewFloor,
    StatChange
  }

  public Type type { get; set; }

  public Trigger (Type _type) {
    type = _type;
  }

}
