using UnityEngine;
using System.Collections;

public class EventView : BaseBehaviour {

  public PlayerEvent playerEvent { get; set; }
  bool hasTriggered = false;
  Rect screenRect;

  void Awake () {
    screenRect = new Rect(0f, 0f, Screen.width + 1, Screen.height + 1);
  }

  // Update is called once per frame
  void Update () {
    DetectVisibleTrigger();
  }

  void DetectVisibleTrigger () {

    if (hasTriggered) {
      return;
    } else {
      if (playerEvent.Triggers.Count < 1) {
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

    // we made it through each corner visible, so we should
    // trigger visibility.

    hasTriggered = true;
    playerEvent.Trigger();
  }

}
