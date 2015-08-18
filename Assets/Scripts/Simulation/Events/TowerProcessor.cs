using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerProcessor {

  Simulation sim;
  Floor floor;

  TowerState state {
    get {
      return sim.player.towerState;
    }
  }

  public TowerProcessor (Simulation _sim) {
    sim = _sim;
    floor = sim.player.towerState.floor;
  }

  public List<PlayerEvent> Continue () {
    var newEvents = new List<PlayerEvent>();

    newEvents.AddRange(VentureForth());

    return newEvents;
  }

  List<PlayerEvent> VentureForth () {
    var newEvents = new List<PlayerEvent>();

    //newEvents.Add(PlayerEvent.Info ("You venture forth..."));

    string content = Roll.Hash(sim.player.floor.content);
    Debug.Log ("Chose content " + content);
    
    // TODO: Inject atmosphere text randomly
    
    if (content == Constants.mobContentKey) {
      var battleProcessor = new BattleProcessor(sim);
      var mob = floor.RandomMob();
      newEvents.AddRange(EncounterMob(mob));
      newEvents.AddRange(battleProcessor.StartBattle(mob));
    }
    
    if (content == Constants.interactibleContentKey) {
      var interactionProcessor = new InteractionProcessor(sim);
      var interactible = floor.RandomInteractible(); 
      newEvents.AddRange(interactionProcessor.StartInteraction(interactible));
    }

    if (content == Constants.roomContentKey) {
      var roomProcessor = new RoomProcessor(sim);
      newEvents.AddRange(roomProcessor.DoorChoice());
    }

    return newEvents;
  }

  List<PlayerEvent> EntranceEvents () {
    var newEvents = new List<PlayerEvent>();
    newEvents.Add(TowerEntranceEvent());
    newEvents.Add(AtmosphereEvent());
    return newEvents;
  }
  
  PlayerEvent TowerEntranceEvent () {
    return new PlayerEvent("You step into the tower.");
  }
  
  PlayerEvent AtmosphereEvent () {
    return new PlayerEvent(floor.RandomAtmosphereText());
  }

  List<PlayerEvent> EncounterMob (Mob mob) {
    if (sim.player.encounteredMobs.Contains(mob.template.Key)) {
      return new List<PlayerEvent>();
    }

    sim.player.encounteredMobs.Add(mob.template.Key);
    return new List<PlayerEvent>(){ PlayerEvent.Story("[New enemy discovered] " + mob.name) };
  }

}
