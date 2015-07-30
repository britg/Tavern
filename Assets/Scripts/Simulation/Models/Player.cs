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
  public TowerState tower;
  public PlayerEvent lastEvent;
  public List<string> encounteredMobs;

  public Player () {
    Resources = new Dictionary<string, Resource>();
    Adventurers = new Dictionary<string, Adventurer>();
    Buildings = new Dictionary<string, Building>();
    Stats = new Dictionary<string, Stat>();
    Slots = new Dictionary<string, Slot>();

    // TODO: Load this from persistent storage
    location = Location.Town;
    tower = new TowerState();
    tower.floorNum = 1;
    tower.hasEnteredTower = false;
    encounteredMobs = new List<string>();
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
    return stat.Value + StatAdditionsFromEquipment(key);
  }

  public float StatAdditionsFromEquipment (string key) {
    float sum = 0f;
    foreach (KeyValuePair<string, Slot> p in Slots) {
      var e = p.Value.Equipment;
      if (e != null) {
        sum += e.StatValue(key);
      }
    }

    return sum;
  }

  public string LocationName () {
    if (location == Player.Location.Town) {
      return "";
    }

    if (location == Player.Location.Shop) {
      return "Shops";
    }

    if (location == Player.Location.Tower) {
      var floor = FloorTemplate.GetFloor(tower.floorNum);
      return string.Format("{0}, floor {1}", floor.name, tower.floorNum);
    }

    return "";
  }

}
