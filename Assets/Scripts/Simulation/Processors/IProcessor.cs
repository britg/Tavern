using UnityEngine;
using System.Collections;

public interface IProcessor {

  void Start (Simulation _sim);
	void Update (float deltaTime);
  void OnSecond();
  void OnMinute();
  void OnHour();
  void OnDay();
  void OnNight();
}
