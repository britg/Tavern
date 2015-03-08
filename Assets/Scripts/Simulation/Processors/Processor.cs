using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Processor : IProcessor {

  protected Simulation sim;

  public virtual void Init (Simulation _sim) {
    sim = _sim;
  }

  public virtual void Start () {}

  public virtual void Update (float deltaTime) {}
  public virtual void OnSecond () {}
  public virtual void OnMinute () {}
  public virtual void OnHour () {}
  public virtual void OnDay () {}
  public virtual void OnNight () {}

}