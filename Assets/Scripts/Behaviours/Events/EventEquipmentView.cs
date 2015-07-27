using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class EventEquipmentView : EventView {

  public Text title;
  public Text description;
  public GameObject choicesObj;

  public Equipment eq {
    get {
      return playerEvent.Equipment;
    }
  }

	// Use this for initialization
	void Start () {
    if (playerEvent == null) {
      return;
    }

    UpdateEquipment();
    NotificationCenter.AddObserver(this, Constants.OnUpdateEvents);

	}

  public void UpdateEquipment () {
    if (playerEvent.Equipment == null) {
      title.color = Color.gray;
    } else {
      var str = string.Format("[{0}]", playerEvent.Content); 
      title.text = str;
      title.color = eq.Rarity.Color;
      description.text = StatsString();
    }
  }

  string StatsString () {
    string str = "";
    foreach (KeyValuePair<string, Stat> p in eq.Stats) {
      var stat = p.Value;
      var pol = "+";
      if (stat.Value < 0f) {
        pol = "-";
      }
      str += string.Format("{1} {2}", pol, stat.Value, stat.Abbr);
    }

    return str;
  }

  void OnUpdateEvents (Notification n) {
    UpdateEquipment();
    if (playerEvent.chosenKey != null) {
      choicesObj.SetActive(false);
    }
  }

	
}
