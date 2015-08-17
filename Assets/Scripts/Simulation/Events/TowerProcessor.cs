using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerProcessor {

  const string _mob = "mob";
  const string _interactible = "interactible";
  const string _room = "room";

  Simulation sim;
  Floor floor;

  TowerState state {
    get {
      return sim.player.tower;
    }
  }

  public TowerProcessor (Simulation _sim) {
    sim = _sim;
    floor = sim.player.tower.CurrentFloor;
  }

  public List<PlayerEvent> Continue () {
    var newEvents = new List<PlayerEvent>();

    // Just entered tower this session
    if (!state.hasEnteredTower) {
      state.hasEnteredTower = true;
      //newEvents.AddRange(EntranceEvents());
    }

    if (state.currentMob != null) {
      var battleProcessor = new BattleProcessor(sim);
      newEvents.AddRange(battleProcessor.Continue());
    }

    if (state.currentInteractible != null) {
      var interactionProcessor = new InteractionProcessor(sim);
      newEvents.AddRange(interactionProcessor.Continue());
    }

    // if our events don't end with a choice
    if (newEvents.Count < 1 || !newEvents[newEvents.Count - 1].hasChoices) {
      newEvents.AddRange(VentureForth());
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

  List<PlayerEvent> VentureForth () {
    var newEvents = new List<PlayerEvent>();

    //newEvents.Add(PlayerEvent.Info ("You venture forth..."));

    var proportions = iTween.Hash(
      _mob, 100f
      /*
      _interactible, 25,
      _room, 25
      */
    );

    string happening = Roll.Hash(proportions);
    
    Debug.Log ("Chose happening " + happening);
    
    // TODO: Inject atmosphere text randomly
    
    if (happening == _mob) {
      var battleProcessor = new BattleProcessor(sim);
      var mob = floor.RandomMob();
//      newEvents.AddRange(EncounterMob(mob));
      newEvents.AddRange(battleProcessor.StartBattle(mob));
    }
    
    if (happening == _interactible) {
      var interactionProcessor = new InteractionProcessor(sim);
      var interactible = floor.RandomInteractible(); 
      newEvents.AddRange(interactionProcessor.StartInteraction(interactible));
    }

    return newEvents;
  }

  public string GetHappening () {

    var proportions = iTween.Hash(
      _mob, 100
      /*
      _interactible, 25,
      _room, 25
      */
    );

    return Roll.Hash(proportions);
  }

  List<PlayerEvent> EncounterMob (Mob mob) {
    if (sim.player.encounteredMobs.Contains(mob.template.Key)) {
      return new List<PlayerEvent>();
    }

    sim.player.encounteredMobs.Add(mob.template.Key);
    return new List<PlayerEvent>(){ PlayerEvent.Info("New enemy discovered! " + mob.name) };
  }

  /* Event Flow

    - Tower entrance message.
    - Hallway message
    - Hallway battles
    - Room message
      - optional closed door prompt
      - locked rooms
    - once cleared enough rooms find stairs up


  */

}
