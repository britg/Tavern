using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mob {

  public MobTemplate template;
  public string name;

  public Dictionary<string, Stat> Stats { get; set; }

  public float currentInitiative;

  public static Mob FromTemplate (MobTemplate template) {
    var mob = new Mob();

    mob.template = template;
    mob.name = template.name;

    mob.Stats = template.Stats;

    foreach (KeyValuePair<string, Stat> pair in mob.Stats) {
      var stat = pair.Value;
      stat.RollBase();
    }

    return mob;
  }

  public Stat GetStat (string key) {
    var mobStat = new Stat(key, 0f);
    if (Stats.ContainsKey(key)) {
      mobStat = Stats[key];
    } else {
      Stats[key] = mobStat;
    }
    
    return mobStat;
  }

  public float GetStatValue (string key) {
    var stat = GetStat(key);
    return stat.Value;
  }
	
}
