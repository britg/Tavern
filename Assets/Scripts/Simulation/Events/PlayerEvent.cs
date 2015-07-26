using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerEvent {

  public enum Type {
    Info,
    Equipment,
    Transition,
    Choice
  }

  public int Id { get; set; }
  public string Title { get; set; }
  public string Content { get; set; }
  public Type type = Type.Info;

  public Equipment Equipment { get; set; }
  public List<Trigger> Triggers = new List<Trigger>();

  public Choice firstChoice;
  public Choice secondChoice;

  public string chosenKey;

  public bool hasTriggered = false;
  public bool conditionsSatisfied = true;

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
    ev.Equipment = e;
    return ev;
  }

  public static PlayerEvent Choice (string text, Choice firstChoice, Choice secondChoice) {
    PlayerEvent ev = new PlayerEvent(text);
    ev.type = Type.Choice;
    ev.firstChoice = firstChoice;
    ev.secondChoice = secondChoice;
    ev.conditionsSatisfied = false;
    return ev;
  }

  public void Update () {
    // shim for when this gets persisted....
  }

}
