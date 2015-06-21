using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerEvent {

  public enum Type {
    Info,
    Equipment,
    Transition
  }

  public int Id { get; set; }
  public string Title { get; set; }
  public string Content { get; set; }
  public Type type = Type.Info;

  public Equipment Equipment { get; set; }
  public List<Trigger> Triggers = new List<Trigger>();

  public bool hasTriggered = false;

  public PlayerEvent (string content) {
    Content = content;
    type = Type.Info;
  }

  public static PlayerEvent Transition (string name) {
    PlayerEvent ev = new PlayerEvent(name);
    ev.type = Type.Transition;

    return ev;
  }

  public static PlayerEvent Loot (Equipment e) {
    PlayerEvent ev = new PlayerEvent(e.Name);
    ev.type = Type.Equipment;
    return ev;
  }

  public void Update () {
    // shim for when this gets persisted....
  }

}
