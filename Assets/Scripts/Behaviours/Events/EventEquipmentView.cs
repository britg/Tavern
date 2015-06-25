using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class EventEquipmentView : EventView {

  public Text title;
  public Text description;

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
      title.text = "Equipped...";
      title.color = Color.gray;
      enableLeftAction = false;
      enableRightAction = false;
    } else {
      var str = string.Format("[{0}]", playerEvent.Content); 
      title.text = str;
    }
  }

  void OnUpdateEvents (Notification n) {
    UpdateEquipment();
  }

	
}
