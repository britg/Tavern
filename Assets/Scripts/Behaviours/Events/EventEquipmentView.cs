using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class EventEquipmentView : EventView {

  public Text title;
  public Text description;

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
    }
  }

  void OnUpdateEvents (Notification n) {
    UpdateEquipment();
  }

	
}
