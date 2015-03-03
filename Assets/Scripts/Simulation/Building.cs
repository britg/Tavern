using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Building {

  public enum Type {
    Tavern,
    Provisions,
    Blacksmith,
    Healer,
    Alchemist
  }

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

  public static Building FromTemplate (Type type) {
    Building template = Templates.First( t => t.BuildingType == type );

    Building newBuilding = (Building)Utilities.Copy<Building>(template);
    return newBuilding;
  }

  public string Name { get; set; }
  public Type BuildingType { get; set; }
  public State CurrentState { get; set; }
  public float PercentComplete { get; set; }
  public Tier CurrentTier { get; set; }

  public List<BuildingPrerequisite> Prerequisites { get; set; }
  public int Cost { get; set; }
  public float HourlyExpenses { get; set; }
  public float BuildTime { get; set; }
  public List<Service> Services { get; set; }

  public void CompleteNow () {
    CurrentState = State.Complete;
    PercentComplete = 100f;
  }

}
