using UnityEngine;
using System.Collections;

public class EventEngine {

  Simulation sim;

  public EventEngine (Simulation _sim) {
    sim = _sim;
  }

  public void Input () {
    Debug.Log ("Pulled to quest: generating events");
  }
}
