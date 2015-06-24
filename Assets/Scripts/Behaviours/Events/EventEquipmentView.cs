using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class EventEquipmentView : EventView {

  public Text title;
  public Text description;
  public float swipeFactor = 0.5f;
  public ActionPickUpView actionPickUpView;
  public ActionEquipView actionEquipView;

  RectTransform rectTrans;

	// Use this for initialization
	void Start () {
    if (playerEvent == null) {
      return;
    }
    actionPickUpView.playerEvent = playerEvent;
    actionEquipView.playerEvent = playerEvent;
    rectTrans = GetComponent<RectTransform>();
    UpdateEquipment();
    NotificationCenter.AddObserver(this, Constants.OnUpdateEvents);
	}

  void Update () {
    DetectHorizontalSwipe();
  }

  void DetectHorizontalSwipe () {

    var inbounds = false;

    if (Input.GetMouseButtonDown(0) && DetectInBounds()) {
      CaptureStartOfHorizontalSwipe(Input.mousePosition);
    }

    if (Input.GetMouseButton(0) && isSwiping) {
      CaptureHorizontalSwipe(Input.mousePosition);
    }

    if (Input.GetMouseButtonUp(0) && isSwiping) {
      EndHorizontalSwipe();
    }
  }

  bool DetectInBounds () {
    Vector3[] corners = new Vector3[4];
    rectTrans.GetWorldCorners(corners);
    Debug.Log (corners[0] + " " + corners[1]);

    var pos = Input.mousePosition;
    return (pos.y > corners[0].y && pos.y < corners[1].y);
  }

  Vector3 hSwipeStart;
  Vector3 originalPos;
  bool isSwiping = false;
  void CaptureStartOfHorizontalSwipe (Vector3 startPos) {
    hSwipeStart = startPos;
    originalPos = rectTrans.localPosition;
    isSwiping = true;
  }

  void CaptureHorizontalSwipe (Vector3 currPosition) {
    var delta = currPosition - hSwipeStart;
    var pos = rectTrans.localPosition;
    pos.x += delta.x * swipeFactor;
    rectTrans.localPosition = pos;
    hSwipeStart = currPosition;
  }

  void EndHorizontalSwipe () {
    iTween.MoveTo(gameObject, iTween.Hash ("position", originalPos, "isLocal", true));
    isSwiping = false;
  }

  public void OnDrag (PointerEventData data) {
    var deltaX = data.delta.x;
    var pickUpPos = actionPickUpView.transform.localPosition;
    var equipPos = actionEquipView.transform.localPosition;
    pickUpPos.x += (deltaX * swipeFactor);
    equipPos.x += (deltaX * swipeFactor);
    actionPickUpView.transform.localPosition = pickUpPos;
    actionEquipView.transform.localPosition = equipPos;
  }

  public void OnEndDrag (PointerEventData data) {
    actionPickUpView.transform.localPosition = Vector3.zero;
    actionEquipView.transform.localPosition = Vector3.zero;
  }

  public void UpdateEquipment () {
    if (playerEvent.Equipment == null) {
      title.text = "[Empty]";
    } else {
      var str = string.Format("[{0}]", playerEvent.Content); 
      title.text = str;
    }
  }

  void OnUpdateEvents (Notification n) {
    UpdateEquipment();
  }
	
}
