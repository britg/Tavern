using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventPlayerBasicAttackView : EventView {

  public Text actionText;
  public Text damageText;

  Mob mob;

  void Start () {
    mob = (Mob)playerEvent.data[PlayerEvent.mobKey];
    UpdateActionText();
    UpdateDamage();
  }

  void UpdateActionText () {
    actionText.text = string.Format("X [{0}]", mob.name);
  }

  void UpdateDamage () {
    var damage = (float)playerEvent.data[PlayerEvent.damageKey];
    var currentHp = mob.GetStatValue(Stat.hp);
    var maxHp = mob.GetStat(Stat.hp).Base;
    //damageText.text = string.Format("{0} damage ({1:0.0}/{2:0.0})", damage, currentHp, maxHp);
    damageText.text = string.Format("{0} damage", damage, currentHp, maxHp);
  }

}
