using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameTime {

  float CurrentSeconds { get; set; }

  int DAY_SECONDS = 60 * 60 * 24;
  int HOUR_SECONDS = 60 * 60;
  int MINUTE_SECONDS = 60;

  public int Day {
    get {
      return Mathf.FloorToInt(CurrentSeconds / DAY_SECONDS);
    }
  }

  public int Hour {
    get {
      return Mathf.FloorToInt(CurrentSeconds % DAY_SECONDS / HOUR_SECONDS);
    }
  }

  public int Minute {
    get {
      return Mathf.FloorToInt(CurrentSeconds % HOUR_SECONDS / MINUTE_SECONDS);
    }
  }

  public int Seconds {
    get {
      return Mathf.FloorToInt(CurrentSeconds % MINUTE_SECONDS);
    }
  }

  public void AddTime (float amount) {
    CurrentSeconds += amount;
  }

  public void AddTime (int amount) {
    AddTime((float)amount);
  }

  public override string ToString () {
    return string.Format("Day {0}, {1:D2}:{2:D2}:{3:D2}", Day, Hour, Minute, Seconds);
  }

}