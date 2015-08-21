using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {

  // data keys for persistence
  public const string currentFloorKey = "currentFloor";
  public const string currentRoomKey = "currentRoom";
  public const string currentInteractibleKey = "currentInteractible";
  public const string currentMobKey = "currentMob";
  public const string currentEventKey = "currentEvent";
  public const string currentChoiceKey = "currentChoice";
  public const string currentInitiativeKey = "currentInitiative";
  public const string encounteredMobsKey = "encounteredMobs";
  public const string lastBattleMoveKey = "lastBattleMove";

  public Dictionary<string, Resource> Resources { get; set; }
  public Dictionary<string, Stat> Stats { get; set; }
  public Dictionary<string, Slot> Slots { get; set; }

  public Hashtable data;

  public Floor currentFloor;
  public Room currentRoom;
  public Interactible currentInteractible;
  public Mob currentMob;
  public PlayerEvent currentEvent;
  public string currentChoice;
  public List<string> encounteredMobs;
  public float currentInitiative;
  public string lastBattleMove;

  public bool currentlyOccupied {
    get {
      return (currentRoom != null 
        || currentInteractible != null 
        || currentMob != null);
    }
  }

  public Player () {
    Resources = new Dictionary<string, Resource>();
    Stats = new Dictionary<string, Stat>();
    Slots = new Dictionary<string, Slot>();

    // TODO: Load this from persistent storage
//    currentFloor = Floor.all[1];
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
    return string.Format("{0}", currentFloor.Name());
  }

  

}
