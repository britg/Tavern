using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerEventEngine {

  Simulation sim;
  FloorTemplate floor;

  TowerState state;

  public TowerEventEngine (Simulation _sim) {
    sim = _sim;
    state = sim.player.tower;
    floor = FloorTemplate.GetFloor(state.floor);
  }

  public List<PlayerEvent> Events () {
    var newEvents = new List<PlayerEvent>();

    if (!state.hasEnteredTower) {
      state.hasEnteredTower = true;
      newEvents.AddRange(EntranceEvents());
    }




    

    Mob mob = floor.RandomMob();
    newEvents.Add(new PlayerEvent("Enemies encountered: [" + mob.template.name + "]"));

    return newEvents;
  }

  List<PlayerEvent> EntranceEvents () {
    var newEvents = new List<PlayerEvent>();
    newEvents.Add(TowerEntranceEvent());
    newEvents.Add(AtmosphereEvent());
    newEvents.Add(HallwayEvent());
    return newEvents;
  }


  PlayerEvent TowerEntranceEvent () {
    return new PlayerEvent("You step into the tower.");
  }

  PlayerEvent AtmosphereEvent () {
    return new PlayerEvent(floor.RandomAtmosphereText());
  }

  PlayerEvent HallwayEvent () {
    return new PlayerEvent("A dark hallway stretches before you. You venture forth in search of stairs...");
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
