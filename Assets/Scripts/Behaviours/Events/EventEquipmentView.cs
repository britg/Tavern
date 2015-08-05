using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class EventEquipmentView : EventView {

  public Text title;
  public Text description;

  Sprite originalPullRightSprite;
  Sprite originalPullLeftSprite;
  public Image pullRightIcon;
  public Text pullRightLabel;
  public Image pullLeftIcon;
  public Text pullLeftLabel;
  public Sprite checkSprite;

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

    originalPullLeftSprite = pullLeftIcon.sprite;
    originalPullRightSprite = pullRightIcon.sprite;

    UpdateEquipment();
    NotificationCenter.AddObserver(this, Constants.OnUpdateEvents);

	}

  void OnUpdateEvents (Notification n) {
    UpdateEquipment();
    UpdateActions();
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

  void UpdateActions () {
    if (playerEvent.chosenKey != null) {

      // Pulled left
      if (rightActionView.actionName == playerEvent.chosenKey) {
        pullLeftIcon.sprite = checkSprite;
        pullLeftLabel.text = "Equipped";
        pullRightIcon.sprite = null;
        pullRightLabel.text = "";
      } 
      
      // pulled right
      else {
        pullRightIcon.sprite = checkSprite;
        pullRightLabel.text = "Picked Up";
        pullLeftIcon.sprite = null;
        pullLeftLabel.text = "";
      }

      enableLeftAction = false;
      enableRightAction = false;

    }
  }


}
