using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameTime {

  TimeConfig config;

  float CurrentSeconds { get; set; }

  int DAY_SECONDS = 60 * 60 * 24;
  int HOUR_SECONDS = 60 * 60;
  int MINUTE_SECONDS = 60;

  float currentMinuteProgress = 0f;
  float currentHourProgress = 0f;

  public delegate void ChangeEventHandler ();

  public event ChangeEventHandler MinuteChange;
  public event ChangeEventHandler HourChange;
  public event ChangeEventHandler DayChange;
  public event ChangeEventHandler NightChange;

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

  public GameTime (TimeConfig timeConfig) {
    config = timeConfig;
    HourChange += UpdateDayNight;
    AddTime(timeConfig.start_seconds);
  }

  public void AddTime (int amount) {
    AddTime((float)amount);
  }

  public void AddTime (float amount) {
    CurrentSeconds += amount;
    UpdateMinuteProgress(amount);
    UpdateHourProgress(amount);
  }

  void UpdateMinuteProgress (float amount) {
    currentMinuteProgress += amount;
    if (currentMinuteProgress >= MINUTE_SECONDS) {
      if (MinuteChange != null) {
        MinuteChange();
      }
      currentMinuteProgress = 0f;
    }
  }

  void UpdateHourProgress (float amount) {
    currentHourProgress += amount;
    if (currentHourProgress >= HOUR_SECONDS) {
      if (HourChange != null) {
        HourChange();
      }
      currentHourProgress = 0f;
    }
  }

  void UpdateDayNight () {
    if (Hour == config.day_start_hour) {
      if (DayChange != null) {
        DayChange();
      }
    }

    if (Hour == config.night_start_hour) {
      if (NightChange != null) {
        NightChange();
      }
    }
  }

  public override string ToString () {
    return string.Format("Day {0}, {1:D2}:{2:D2}:{3:D2}", Day, Hour, Minute, Seconds);
  }

}