using UnityEngine;
using System.Collections;

public class Simulation {

  public Player player;

  public void Setup (SimulationConfig config) {
    config.LoadModels();
    player = config.LoadPlayer();
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
