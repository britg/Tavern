using UnityEngine;
using System.Collections;

public class QuestMarkerView : BaseBehaviour {

  QuestMarkerState state = QuestMarkerState.Placing;

  public GameObject radiusObj;

	// Use this for initialization
	void Start () {
    ConfigureRadius();
    ToggleRadius();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void ConfigureRadius () {
    var radius = sim.config.defaultExplorationRadius;
    radiusObj.transform.localScale = new Vector3(radius, 0.01f, radius);
  }

  void ToggleRadius () {
    if (QuestMarkerState.Placing == state) {
      radiusObj.SetActive(true);
    }
  }
}
