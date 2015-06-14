using UnityEngine;
using System.Collections;

public class Adventurer {

  public AdventurerClass Class { get; set; }
  public AdventurerState State { get; set; }

  public string id { get; set; }
  public string Name { get; set; }
  public int Level { get; set; }
  public int Experience { get; set; }

  public Vector3 Location { get; set; }
  public Vector3 Destination { get; set; }
  public float Speed { get; set; } // units/hour
  public float SpeedPerSecond { get {
      return Speed / Constants.SecondsPerHour;
    } }

  public Adventurer (string className) {
    Class = AdventurerClass.all[className];
    id = System.Guid.NewGuid().ToString();
    State = AdventurerState.Idle;
    Location = Vector3.zero;
  }

  public override string ToString () {
    return string.Format("Adventurer: {0} {1}, {2} @ {3}", Class.Name, Name, State, Location);
  }
}
