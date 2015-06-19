using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ScrollView : BaseBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

  public GameObject eventList;
  FeedView feedView;
  ScrollRect scrollRect;

  float initialY = 0f;
  float currentDelta = 0f;
  public float pullTrigger = 100f;

	// Use this for initialization
	void Start () {
    scrollRect = GetComponent<ScrollRect>();
    feedView = eventList.GetComponent<FeedView>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void OnBeginDrag (PointerEventData data) {
    initialY = eventList.transform.position.y;
  }

  public void OnDrag (PointerEventData data) {
    Debug.Log ("On drag " + scrollRect.verticalNormalizedPosition + " " + eventList.transform.position.y);
    currentDelta = initialY - eventList.transform.position.y;

    if (currentDelta >= pullTrigger) {
      feedView.OnPull();
      Reset();
    }
  }

  public void OnEndDrag (PointerEventData data) {
    Reset();
  }

  void Reset () {
    initialY = 0f;
    currentDelta = 0f;
  }
}
