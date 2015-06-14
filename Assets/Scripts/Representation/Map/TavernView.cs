using UnityEngine;
using System.Collections;

public class TavernView : BaseBehaviour {

  Building tavern;

	// Use this for initialization
	void Start () {
	  
	}
	
	// Update is called once per frame
	void Update () {
	  
	}

  public void SetTavern (Building _tavern) {
    tavern = _tavern;
    Debug.Log(tavern.Type.Name);
    tavern.Name = "Cheese";
  }
}
