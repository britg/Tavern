using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventEquipmentView : EventView {

  public Text title;
  public Text description;
  public ActionEquipView actionEquipView;

	// Use this for initialization
	void Start () {
    if (playerEvent == null) {
      return;
    }
    actionEquipView.playerEvent = playerEvent;
    var str = string.Format("[{0}]", playerEvent.Content);
    title.text = str;
	}
	
}
