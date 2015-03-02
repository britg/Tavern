using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Processor : IProcessor {

  protected Simulation sim;

  public virtual void Start (Simulation _sim) {
    sim = _sim;
  }

  public virtual void Update (float deltaTime) {}
  public virtual void StartSecond () {}
  public virtual void StartMinute () {}
  public virtual void StartHour () {}
  public virtual void StartDay () {}
  public virtual void StartNight () {}

}