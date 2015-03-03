using UnityEngine;
using System.Collections;

public class SimulationRunner : MonoBehaviour {

  public Simulation sim;

  void Awake () {
    GetSim();
  }

	// Use this for initialization
	void Start () {
    sim.Start();
	}

	// Update is called once per frame
	void Update () {
    sim.Update(Time.deltaTime);
	}

  public void Pause () {
    sim.Pause();
  }

  public Simulation GetSim() {
    if (sim == null) {
      sim = new Simulation();
      sim.Setup();
    }
    return sim;
  }
}
