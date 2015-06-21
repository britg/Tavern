using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class EventEquipmentView : EventView, IDragHandler, IEndDragHandler {

  public Text title;
  public Text description;
  public float swipeFactor = 0.5f;
  public ActionPickUpView actionPickUpView;
  public ActionEquipView actionEquipView;

	// Use this for initialization
	void Start () {
    if (playerEvent == null) {
      return;
    }
    actionPickUpView.playerEvent = playerEvent;
    actionEquipView.playerEvent = playerEvent;
    var str = string.Format("[{0}]", playerEvent.Content);
    title.text = str;
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
	
}
