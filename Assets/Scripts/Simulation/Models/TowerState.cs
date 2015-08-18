using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerState {

  public bool hasEnteredTower; //= false;

  public int floorNum; //= 1;
  public List<string> roomsCleared; //= 0;
  public Room currentRoom; //= 0;

  // Interactible
  public Interactible currentInteractible;

  // battle
  public Mob currentMob;
  public string lastBattleMove;

  Floor _currentFloor;
  public Floor CurrentFloor {
    get {
      if (_currentFloor == null) {
        _currentFloor = Floor.GetFloor(floorNum);
      }
      return _currentFloor;
    }
  }

  public Dictionary<string, float> content {
    get {
      if (currentRoom != null) {
        return currentRoom.content;
      }

      return CurrentFloor.content;
    }
  }

}
