using UnityEngine;
using System.Collections;

public class Adventurer {

  public AdventurerClass Class { get; set; }
  public AdventurerState State { get; set; }

  public string id { get; set; }
  public string Name { get; set; }
  public int Level { get; set; }
  public int Experience { get; set; }

  public Adventurer (string className) {
    Class = AdventurerClass.all[className];
    id = System.Guid.NewGuid().ToString();
    Level = 1;
    Experience = 0;
    State = AdventurerState.Idle;
  }

  public override string ToString () {
    return string.Format("Adventurer: {0} {1}, {2}", Class.Name, Name, State);
  }
}
