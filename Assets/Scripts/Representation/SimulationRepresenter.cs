using UnityEngine;
using System.Collections;

public class SimulationRepresenter : BaseBehaviour {

	// Use this for initialization

  void Awake () {
    NotificationCenter.AddObserver(this, Constants.OnAdventurerCreated);
    NotificationCenter.AddObserver(this, Constants.OnTavernCreated);
  }

	void Start () {
	}


  void OnAdventurerCreated (Notification n) {
    Debug.Log("Receiving adventurer created " + n);
  }

  void OnTavernCreated (Notification n) {
    Debug.Log("On Tavern created");
  }

}
