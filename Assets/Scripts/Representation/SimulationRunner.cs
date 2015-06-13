using UnityEngine;
using System.Collections;

public class SimulationRunner : MonoBehaviour {

  public Simulation sim;

  public Simulation GetSim() {
    if (sim == null) {
      sim = new Simulation();
      sim.Setup();
    }
    return sim;
  }


	// Use this for initialization
  void Start () {
    GetSim();
    Application.targetFrameRate = 60;
    sim.Start();
	}

	// Update is called once per frame
	void Update () {
    sim.Update(Time.deltaTime);
	}

  public void Pause () {
    sim.Pause();
  }

  public void Resume () {
    sim.Resume();
  }

  public void AdjustCurrentSpeed (float value) {
    sim.AdjustCurrentSpeed(Mathf.Floor(value));
  }
}
