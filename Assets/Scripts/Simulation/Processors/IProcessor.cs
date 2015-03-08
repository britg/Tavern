using UnityEngine;
using System.Collections;

public interface IProcessor {

  void Init (Simulation _sim);
  void Start ();
	void Update (float deltaTime);
  void OnSecond();
  void OnMinute();
  void OnHour();
  void OnDay();
  void OnNight();
}
