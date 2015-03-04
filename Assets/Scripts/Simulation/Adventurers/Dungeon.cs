using UnityEngine;
using System.Collections;

// Dungeon has potential loot value
// Adventuerer preparedness rating based on your town
// Their loot haul is a function of their preparedness vs the dungeon loot score
// preparedness is determined from wants/needs commodities

public class Dungeon  {

  public string Name { get; set; }
  public int Level { get; set; }
  public float PercentCompleted  { get; set; }

}
