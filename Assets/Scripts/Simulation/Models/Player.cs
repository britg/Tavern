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

  public float currentInitiative;

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
    return stat.Value;
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
      var floor = FloorTemplate.GetFloor(tower.floorNum);
      return string.Format("{0}, {1} floor", floor.name, AddOrdinal(tower.floorNum));
    }

    return "";
  }

  public static string AddOrdinal (int num) {
    if (num <= 0) return num.ToString();

    switch (num % 100) {
      case 11:
      case 12:
      case 13:
        return num + "th";
    }

    switch (num % 10) {
      case 1:
        return num + "st";
      case 2:
        return num + "nd";
      case 3:
        return num + "rd";
      default:
        return num + "th";
    }

  }

}
