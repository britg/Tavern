using UnityEngine;
using System.Collections;

public class ExplorationQuestCreator {

  Simulation sim;

  public ExplorationQuestCreator (Simulation _sim) {
    sim = _sim;
  }

  public void Create (Vector3 location) {
    Debug.Log("Creating quest at location " + location);

    var quest = new Quest();
    quest.Name = "Explore!";
    quest.Location = location;

    sim.questList.Add(quest);
  }

}
