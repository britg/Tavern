using UnityEngine;
using System.Collections;

public class Mob {

  public MobTemplate template;
  public string name;
  public int hitpoints;

  public static Mob FromTemplate (MobTemplate template) {
    var mob = new Mob();

    mob.template = template;
    mob.name = template.name;
    mob.hitpoints = Random.Range(template.minHitpoints, template.maxHitpoints);

    return mob;
  }
	
}
