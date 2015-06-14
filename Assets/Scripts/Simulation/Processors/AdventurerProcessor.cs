using UnityEngine;
using System.Collections;

public class AdventurerProcessor : Processor {

  Adventurer adv;

  public AdventurerProcessor (Adventurer _adv, Simulation _sim) {
    adv = _adv;
    sim = _sim;
  }

  public override void Start (Simulation _sim) {

    base.Start(_sim);
  }

  public override void Update (float deltaTime) {
    if (adv.State == AdventurerState.Travelling) {
      UpdateLocation(deltaTime);
    }
    base.Update(deltaTime);
  }

  public override void OnMinute() {
    //Debug.Log(adv.ToString());
    ProcessState();
    base.OnMinute();
  }

  void ProcessState () {
  }

  void SetRandomDestination () {
    var dest = new Vector3(Random.Range(-1f, 1f) * Random.Range(0f, 100f),Random.Range(-1f, 1f) *  Random.Range(0f, 100f), 0f);
    adv.Destination = dest;
  }

  void UpdateLocation (float deltaTime) {
    var dir = adv.Destination - adv.Location;
    var add = dir.normalized * adv.SpeedPerSecond * deltaTime;
    adv.Location += add;
  }


}
