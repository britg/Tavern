using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Building {

  public enum State {
    Building,
    Complete
  }

  public enum Tier {
    Basic,
    Upgraded,
    Advanced
  }

  public static List<Building> Templates { get; set; }

  public static Building FromTemplate (string name) {
    Building template = Templates.First( t => t.Name == name );

    Building newBuilding = (Building)Utilities.Copy<Building>(template);
    return newBuilding;
  }

  public string Name { get; set; }
  public State CurrentState { get; set; }
  public float PercentComplete { get; set; }
  public Tier CurrentTier { get; set; }

  public List<BuildingPrerequisite> Prerequisites { get; set; }
  public int BuildCost { get; set; }
  public float BuildTime { get; set; }
  public float HourlyMaintenanceCost { get; set; }

  public List<Commodity> Commodities { get; set; }

  public void CompleteNow () {
    CurrentState = State.Complete;
    PercentComplete = 100f;
  }

}
