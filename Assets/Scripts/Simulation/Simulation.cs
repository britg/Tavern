using UnityEngine;
using System.Collections;

public class Simulation {

  public Player player;

  public void Setup () {
    var config = new SimulationConfig();
    config.LoadModels();
    player = config.LoadPlayer();

    Debug.Log ("initial gold is " + config.initialGold);
  }

  public void Start () {
  }

  public void Update (float deltaTime) {
  }

  public void Pause () {
  }

  public void Resume () {
  }

  public void AdjustCurrentSpeed (float multiplier) {

  }

}
