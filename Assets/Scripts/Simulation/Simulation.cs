using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Simulation {

//  public SimulationConfig config;

  float previousSpeed;
  public float CurrentSpeed { get; set; }
  public GameTime GameTime { get; set; }
  public float UpdateDelta { get; set; }

  public World World { get; private set; }
  public Player Player { get; private set; }
  public Economy Economy { get; private set; }

  List<IProcessor> processorRegistry;

  public void Setup () {
//    config = SimulationConfig.Instance;
//    Debug.Log(config);

    World = new World();
    SetupTime();
    SetupBuildings();
    SetupPlayer();
    SetupProcessorRegistry();
  }

  public void Start () {
    InitProcessors();
    StartProcessors();
    SmartConsole.ExecuteLine("show.fps True");
  }

  void SetupTime () {
//    CurrentSpeed = config.TimeConfig.start_speed;
//    GameTime = new GameTime(config.TimeConfig);
//    GameTime.MinuteChange += OnMinute;
//    GameTime.HourChange += OnHour;
//    GameTime.DayChange += OnDay;
//    GameTime.NightChange += OnNight;
  }

  void SetupBuildings () {
//    Building.Templates = config.BuildingConfig.Templates;
  }

  void SetupPlayer () {
//    Player = new Player(config.PlayerConfig);
//    Simulation.Log(Player.Buildings[0]);
  }

  void SetupProcessorRegistry () {
    processorRegistry = new List<IProcessor>();
    processorRegistry.Add(new CashflowProcessor());
    processorRegistry.Add(new EconomyProcessor());
  }

  void InitProcessors () {
    foreach (IProcessor proc in processorRegistry) {
      proc.Init(this);
    }
  }

  void StartProcessors () {
    foreach (IProcessor proc in processorRegistry) {
      proc.Start();
    }
  }

  public void Update (float deltaTime) {
//    UpdateDelta = deltaTime * CurrentSpeed;
//    GameTime.AddTime(UpdateDelta);
//
//    foreach (IProcessor processor in processorRegistry) {
//      processor.Update(UpdateDelta);
//    }
  }

  public void OnSecond () {
    foreach (IProcessor processor in processorRegistry) {
      processor.OnSecond();
    }
  }

  public void OnMinute () {
    foreach (IProcessor processor in processorRegistry) {
      processor.OnMinute();
    }
  }

  public void OnHour () {
    foreach (IProcessor processor in processorRegistry) {
      processor.OnHour();
    }
  }

  public void OnDay () {
    Simulation.Log("Day starting");
    foreach (IProcessor processor in processorRegistry) {
      processor.OnDay();
    }
  }

  public void OnNight () {
    Simulation.Log("Night starting");
    foreach (IProcessor processor in processorRegistry) {
      processor.OnDay();
    }
  }

  public void Pause () {
    previousSpeed = CurrentSpeed;
    CurrentSpeed = 0f;
  }

  public void Resume () {
    CurrentSpeed = previousSpeed;
  }

  public void AdjustCurrentSpeed (float value) {
    CurrentSpeed = value;
  }

  public static void Log (object obj) {
    Debug.Log(obj);
  }

}
