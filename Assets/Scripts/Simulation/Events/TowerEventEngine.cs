using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerEventEngine {

  Simulation sim;
  FloorTemplate floor;

  TowerState state {
    get {
      return sim.player.tower;
    }
  }

  public TowerEventEngine (Simulation _sim) {
    sim = _sim;
    floor = FloorTemplate.GetFloor(state.floorNum);
  }

  public List<PlayerEvent> Continue () {
    var newEvents = new List<PlayerEvent>();

    // Just entered tower this session
    if (!state.hasEnteredTower) {
      state.hasEnteredTower = true;
      newEvents.AddRange(EntranceEvents());
    }

    // Continuing a battle
    if (state.currentMobGroup != null) {
      var battleEventEngine = new BattleEventEngine(sim);
      newEvents.AddRange(battleEventEngine.Continue());
    }

    string happening = GetHappening();

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

  public string GetHappening () {
    /* chance for:
          - mob 
          - room
          - interactible
            - chest
            - dead body
            - set of vases against the walll

    */
    
    var happenings = iTween.Hash(
      "mob_group", 50,
      "interactible", 25,
      "room", 25
      );
    
    int rand = Random.Range(0, 100);

    return "mob_group";
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
