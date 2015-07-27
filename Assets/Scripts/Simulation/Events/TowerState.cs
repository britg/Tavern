using UnityEngine;
using System.Collections;

public class TowerState {

  public bool hasEnteredTower = false;

  public int floor = 1;
  public int rooms = 10;
  public int roomsCleared = 0;
  public int currentRoom = 0;

  public int minEnemiesPerRoom = 3;
  public int maxEnemiesPerRoom = 6;

  public int minBossesPerRoom = 0;
  public int maxBossesPerRoom = 1;

}
