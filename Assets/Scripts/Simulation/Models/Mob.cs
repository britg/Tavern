using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mob {

  public MobTemplate template;
  public string name;

  public Dictionary<string, Stat> Stats { get; set; }
  public Hashtable combatProfile;
  public float currentInitiative;

  public static Mob FromTemplate (MobTemplate template) {
    var mob = new Mob();

    mob.template = template;
    mob.name = template.name;

    mob.Stats = new Dictionary<string, Stat>();
    foreach (KeyValuePair<string, Stat> pair in template.Stats) {
      var statKey = pair.Key;
      var templateStat = pair.Value;
      templateStat.RollBase();
      mob.Stats[statKey] = new Stat(statKey, templateStat.Value);
    }
    mob.combatProfile = template.combatProfile;

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

  public void ChangeStat (string key, float amount) {
    var s = GetStat(key);
    s.Change(amount);
    Stats[key] = s;
  }
	
}
