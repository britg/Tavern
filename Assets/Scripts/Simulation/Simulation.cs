using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Simulation {

  public float CurrentSpeed { get; set; }
  public GameTime GameTime { get; set; }
  public float UpdateDelta { get; set; }

  public World World { get; private set; }
  public Player Player { get; private set; }

  List<IProcessor> processorRegistry;

  public void Setup () {
    World = new World();
    SetupTime();
    SetupBuildings();
    SetupPlayer();
    SetupProcessorRegistry();
  }

  public void Start () {
    StartProcessors();
  }

  void SetupTime () {
    CurrentSpeed = Config.StartSpeed;
    GameTime = new GameTime();
    GameTime.AddTime(Config.StartSeconds);
  }

  void SetupBuildings () {
    Building.Templates = BuildingSetup.BuildingTemplates();
  }

  void SetupPlayer () {
    Player = new Player();
    Player.Gold = Config.StartGold;
    Player.Buildings = new List<Building>();
    // start with a tavern
    Building tavern = Building.FromTemplate(Building.Type.Tavern);
    tavern.CompleteNow();
    Player.Buildings.Add(tavern);
  }

  void SetupProcessorRegistry () {
    processorRegistry = new List<IProcessor>();
    processorRegistry.Add(new CashflowProcessor());
  }

  void StartProcessors () {
    foreach (IProcessor proc in processorRegistry) {
      proc.Start(this);
    }
  }

  public void Update (float deltaTime) {
    UpdateDelta = deltaTime * CurrentSpeed;
    GameTime.AddTime(UpdateDelta);

    foreach (IProcessor processor in processorRegistry) {
      processor.Update(UpdateDelta);
    }
  }

  public void Pause () {
    CurrentSpeed = 0f;
  }

}
