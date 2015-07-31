using UnityEngine;
using System.Collections;

public class InitiativeProcessor {

  static float requiredInitiative = 100f;

  Player player;
  Mob mob;

  public InitiativeProcessor (Player _player, Mob _mob) {
    player = _player;
    mob = _mob;
  }

  public string nextMove () {
    float playerSpd = player.GetStatValue(Stat.spd);
    float mobSpd = mob.GetStatValue(Stat.spd);

    string lastMove = player.tower.lastBattleMove;

    if (lastMove == null || lastMove == "mob") {
      player.currentInitiative += playerSpd;

      if (player.currentInitiative >= requiredInitiative) {
        player.currentInitiative = (player.currentInitiative - requiredInitiative);
        return "player";
      }
    } else {
      mob.currentInitiative += mobSpd;

      if (mob.currentInitiative >= requiredInitiative) {
        mob.currentInitiative = (mob.currentInitiative - requiredInitiative);
        return "mob";
      }
    }

    return nextMove();

  }


}
