using UnityEngine;
using System.Collections;

public class AdventurerView : BaseBehaviour {

  Adventurer adventurer;

  public GameObject warriorPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    transform.position = adventurer.Location;
	}

  public void SetAdventurer (Adventurer _adv) {
    adventurer = _adv;

    // Load the correct view for this Class
    // tmp for just warrior
    if (adventurer.Class.Key == "warrior") {
      GameObject warriorObj = Instantiate(warriorPrefab);
      warriorObj.transform.parent = transform;
      warriorObj.transform.localPosition = Vector3.zero;
    }
  }
}
