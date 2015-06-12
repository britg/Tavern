﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Simulation {

  SimulationConfig config;

  public Player player;
  List<IProcessor> processorRegistry;
  float previousSpeed;
  public float CurrentSpeed { get; set; }
  public GameTime GameTime { get; set; }
  public float UpdateDelta { get; set; }

  public void Setup () {
    config = new SimulationConfig();
    config.LoadModels();
    player = config.LoadPlayer();

    SetupGameTime();

    Debug.Log ("initial gold is " + config.initialGold);
  }

  void SetupGameTime () {
    GameTime = new GameTime(config);
    GameTime.MinuteChange += OnMinute;
    GameTime.HourChange += OnHour;
    GameTime.DayChange += OnDay;
    GameTime.NightChange += OnNight;
  }

  public void Start () {
  }

  public void Update(float deltaTime)
  {
    UpdateDelta = deltaTime * CurrentSpeed;
    GameTime.AddTime(UpdateDelta);

    foreach (IProcessor processor in processorRegistry)
    {
      processor.Update(UpdateDelta);
    }
  }

  public void OnSecond()
  {
    foreach (IProcessor processor in processorRegistry)
    {
      processor.OnSecond();
    }
  }

  public void OnMinute()
  {
    foreach (IProcessor processor in processorRegistry)
    {
      processor.OnMinute();
    }
  }

  public void OnHour()
  {
    foreach (IProcessor processor in processorRegistry)
    {
      processor.OnHour();
    }
  }

  public void OnDay()
  {
    foreach (IProcessor processor in processorRegistry)
    {
      processor.OnDay();
    }
  }

  public void OnNight()
  {
    foreach (IProcessor processor in processorRegistry)
    {
      processor.OnDay();
    }
  }

  public void Pause()
  {
    previousSpeed = CurrentSpeed;
    CurrentSpeed = 0f;
  }

  public void Resume()
  {
    CurrentSpeed = previousSpeed;
  }

  public void AdjustCurrentSpeed(float value)
  {
    CurrentSpeed = value;
  }

}
