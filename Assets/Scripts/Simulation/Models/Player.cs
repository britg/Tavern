using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {

  public enum Location {
    Town,
    Shop,
    Tower
  }

  public Dictionary<string, Resource> Resources { get; set; }
  public Dictionary<string, Adventurer> Adventurers { get; set; }
  public Dictionary<string, Building> Buildings { get; set; }
  public Dictionary<string, Stat> Stats { get; set; }
  public Dictionary<string, Slot> Slots { get; set; }

  public Location location;
  public TowerState towerState;
  public Room currentRoom;
  public Interactible currentInteractible;
  public Mob currentMob;
  public PlayerEvent currentEvent;
  public string currentChoice;
  public List<string> encounteredMobs;
  public float currentInitiative;

  public bool currentlyOccupied {
    get {
      return (currentRoom == null && currentInteractible == null && currentMob == null);
    }
  }

  public Player () {
    Resources = new Dictionary<string, Resource>();
    Adventurers = new Dictionary<string, Adventurer>();
    Buildings = new Dictionary<string, Building>();
    Stats = new Dictionary<string, Stat>();
    Slots = new Dictionary<string, Slot>();

    // TODO: Load this from persistent storage
    location = Location.Tower;
    towerState = new TowerState();
    towerState.floor = Floor.all[1];
    towerState.hasEnteredTower = false;
    encounteredMobs = new List<string>();
    currentInitiative = 0;
  }

  public Stat GetStat (string key) {
    var playerStat = new Stat(key, 0f);
    if (Stats.ContainsKey(key)) {
      playerStat = Stats[key];
    } else {
      Stats[key] = playerStat;
    }

    return playerStat;
  }

  public float GetStatValue (string key) {
    var stat = GetStat(key);
    return stat.current;
  }

  public void ChangeStat (string key, float amount) {
    var s = GetStat(key);
    s.Change(amount);
    Stats[key] = s;
  }

  public void ChangeResource (string key, int amount) {
    var r = Resources[key];
    r.Amount += amount;
    Resources[key] = r;
  }

  public string LocationName () {
    if (location == Player.Location.Town) {
      return "";
    }

    if (location == Player.Location.Shop) {
      return "Shops";
    }

    if (location == Player.Location.Tower) {
      return string.Format("{0}", towerState.floor.Name());
    }

    return "";
  }

  

}
