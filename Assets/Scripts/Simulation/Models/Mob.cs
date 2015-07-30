using UnityEngine;
using System.Collections;

public class Mob {

  public MobTemplate template;
  public string name;

  public int currentHitpoints;
  public int maxHitpoints;

  public int currentAp;
  public int maxAp;
  public int currentInitiative;

  public float dps;

  public static Mob FromTemplate (MobTemplate template) {
    var mob = new Mob();

    mob.template = template;
    mob.name = template.name;

    mob.maxHitpoints = Random.Range(template.minHitpoints, template.maxHitpoints);
    mob.currentHitpoints = mob.maxHitpoints;

    mob.maxAp = Random.Range(template.minAp, template.maxAp);
    mob.currentAp = mob.maxAp;

    mob.dps = Random.Range(template.minDps, template.maxDps);

    return mob;
  }
	
}
