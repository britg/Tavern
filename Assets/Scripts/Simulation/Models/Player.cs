using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {

  public Dictionary<string, ResourceAmount> ResourceAmounts { get; set; }
  public Dictionary<string, Adventurer> Adventurers { get; set; }
  public Dictionary<string, Building> Building { get; set; }


  public Player () {
    ResourceAmounts = new Dictionary<string, ResourceAmount>();
    Adventurers = new Dictionary<string, Adventurer>();
    Building = new Dictionary<string, Building>();
  }

}
