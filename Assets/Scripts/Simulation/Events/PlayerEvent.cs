using UnityEngine;
using System.Collections;

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

  public PlayerEvent (string content) {
    Content = content;
    type = Type.Info;
  }

  public static PlayerEvent Transition (string name) {
    PlayerEvent e = new PlayerEvent(name);
    e.type = Type.Transition;

    return e;
  }

}
