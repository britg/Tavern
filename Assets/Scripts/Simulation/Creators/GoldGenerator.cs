using UnityEngine;
using System.Collections;

public class GoldGenerator {

  Player player;
  Mob mob;
  Interactible interactible;

  public GoldGenerator (Player _player, Mob _mob) {
    player = _player;
    mob = _mob;
  }

  public GoldGenerator (Player _player, Interactible _interactible) {
    player = _player;
    interactible = _interactible;
  }

  public int Interactible () {
    return 10;
  }

  public int Mob () {
    return 10;
  }
	
}
