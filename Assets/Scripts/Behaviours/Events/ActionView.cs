using UnityEngine;
using System.Collections;

public class ActionView : BaseBehaviour {

  public string actionName;

	public PlayerEvent playerEvent {get; set;}
  public bool hasTriggered = false;
  Rect screenRect;

  // Use this for initialization
  void Start () {
    screenRect = new Rect(0f, 0f, Screen.width + 1, Screen.height + 1);
  }
  
  // Update is called once per frame
  void Update () {
    //DetectVisibleTrigger();
  }

  void DetectVisibleTrigger () {

    if (hasTriggered) {
      return;
    } else {
      if (playerEvent == null) {
        hasTriggered = true;
        return;
      }
    }

    Vector3[] corners = new Vector3[4];

    this.GetComponent<RectTransform>().GetWorldCorners(corners);

    foreach (Vector3 corner in corners) {
      if (!screenRect.Contains(corner)) {
        return;
      }
    }

    hasTriggered = true;

    if (playerEvent.hasActions) {
      sim.eventEngine.TriggerAction(playerEvent, actionName);
    }

    if (playerEvent.hasChoices) {
      sim.eventEngine.TriggerChoice(playerEvent, actionName);
    }
  }
}
