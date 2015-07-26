using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class EventChoiceView : EventView {

  public Text title;
  public Text pullRightLabel;
  public Text pullLeftLabel;

  Choice leftChoice;
  Choice rightChoice;

  // Use this for initialization
  void Start () {
    if (playerEvent == null) {
      return;
    }

    NotificationCenter.AddObserver(this, Constants.OnUpdateEvents);
    title.text = playerEvent.Content;

    AssignChoice(playerEvent.firstChoice);
    AssignChoice(playerEvent.secondChoice);

    leftAction.actionName = rightChoice.key;
    rightAction.actionName = leftChoice.key;

    pullLeftLabel.text = leftChoice.label;
    pullRightLabel.text = rightChoice.label;
  }

  void AssignChoice (Choice choice) {

    if (choice.direction == Choice.Direction.Left) {
      leftChoice = choice;
    } else {
      rightChoice = choice;
    }

  }

  void OnUpdateEvents (Notification n) {

  }

}
