using UnityEngine;
using System.Collections;

public class SimulationRepresenter : BaseBehaviour {

  

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    RepresentSim();
	}

  void RepresentSim () {
    RepresentMap();
    RepresentPlayer();
  }

  void RepresentMap () {
    CreateTavernView();
    CreateDungeonViews();
  }

  void CreateTavernView () {
    
  }

  void CreateDungeonViews () {

  }

  void RepresentPlayer () {

  }
}
