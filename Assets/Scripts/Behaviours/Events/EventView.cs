using UnityEngine;
using System.Collections;

public class EventView : BaseBehaviour {

  PlayerEvent _playerEvent;
  public PlayerEvent playerEvent {
    get {
      return _playerEvent;
    }
    set {
      _playerEvent = value;
      if (playerEvent.Triggers.Count < 1) {
        hasTriggered = true;
      }
    }
  }
  bool hasTriggered = false;
  Rect screenRect;

  void Awake () {
    screenRect = new Rect(0f, 0f, Screen.width+1, Screen.height+1);
  }

  // Update is called once per frame
  void Update () {
    if (!hasTriggered) {
      DetectVisibleTrigger();
    }
  }

  void DetectVisibleTrigger () {
    Vector3[] corners = new Vector3[4];

    this.GetComponent<RectTransform>().GetWorldCorners(corners);

    foreach (Vector3 corner in corners) {
      if (!screenRect.Contains(corner)) {
        return;
      }
    }

    // we made it through each corner visible, so we should
    // trigger visibility.

    hasTriggered = true;
    playerEvent.Trigger();
  }

}
