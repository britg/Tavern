using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerEventEngine {

  const string _mob = "mob";
  const string _interactible = "interactible";
  const string _room = "room";

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

    var battleEventEngine = new BattleEventEngine(sim);
    if (state.currentMob != null) {
      newEvents.AddRange(battleEventEngine.Continue());
    }

    var interactibleEventEngine = new InteractibleEventEngine(sim);
    if (state.currentInteractible != null) {
      newEvents.AddRange(interactibleEventEngine.Continue());
    }

    // if our events don't end with a choice
    if (newEvents.Count < 1 || !newEvents[newEvents.Count - 1].hasChoices) {

      string happening = GetHappening();

      Debug.Log ("Chose happening " + happening);
      
      if (happening == _mob) {
        var mob = floor.RandomMob();
        newEvents.AddRange(battleEventEngine.StartBattle(mob));
      }

      if (happening == _interactible) {

      }
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

  public string GetHappening () {
    /* chance for:
          - mob 
          - room
          - interactible
            - chest
            - dead body
            - set of vases against the walll

    */
    
    var proportions = iTween.Hash(
      _mob, 50,
      _interactible, 25,
      _room, 25
    );

    int sum = 0;
    foreach (DictionaryEntry pair in proportions) {
      sum += (int)pair.Value;
    }

    int rand = Random.Range(0, sum);

    int running = 0;
    string chosen = null;
    foreach (DictionaryEntry pair in proportions) {
      running += (int)pair.Value;
      if (rand <= running) {
        chosen = (string)pair.Key;
        break;
      }
    }

    return chosen;
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
