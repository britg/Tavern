using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerState {

  public bool hasEnteredTower; //= false;

  public int floorNum; //= 1;
  public int rooms; //= 10;
  public int roomsCleared; //= 0;
  public int currentRoom; //= 0;

  public int minEnemiesPerRoom; //= 3;
  public int maxEnemiesPerRoom; //= 6;

  public int minBossesPerRoom; //= 0;
  public int maxBossesPerRoom; //= 1;

  public Mob currentMob;
  public Interactible currentInteractible;

}
