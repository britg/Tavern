using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {

  public Dictionary<string, Resource> Resources { get; set; }
  public Dictionary<string, Adventurer> Adventurers { get; set; }
  public Dictionary<string, Building> Buildings { get; set; }
  public Dictionary<string, Stat> Stats { get; set; }
  public Dictionary<string, Slot> Slots { get; set; }

  public Player () {
    Resources = new Dictionary<string, Resource>();
    Adventurers = new Dictionary<string, Adventurer>();
    Buildings = new Dictionary<string, Building>();
    Stats = new Dictionary<string, Stat>();
    Slots = new Dictionary<string, Slot>();
  }

  public void UpdateStats () {
    Debug.Log ("Updating player stats");
  }

}
