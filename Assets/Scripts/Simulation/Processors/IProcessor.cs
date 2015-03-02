using UnityEngine;
using System.Collections;

public interface IProcessor {

  void Start (Simulation _sim);
	void Update (float deltaTime);
  void StartSecond();
  void StartMinute();
  void StartHour();
  void StartDay();
  void StartNight();
}
