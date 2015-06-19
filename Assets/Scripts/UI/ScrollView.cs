using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ScrollView : BaseBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

  enum State {
    Reset,
    Pulling
  }

  public GameObject eventList;
  FeedView feedView;
  ScrollRect scrollRect;

  float initialY = 0f;
  float currentDelta = 0f;
  public float pullTrigger = 150f;
  State state = State.Reset;

	// Use this for initialization
	void Start () {
    scrollRect = GetComponent<ScrollRect>();
    feedView = eventList.GetComponent<FeedView>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void OnBeginDrag (PointerEventData data) {
    if (scrollRect.verticalNormalizedPosition >= 1f) {
      initialY = eventList.transform.position.y;
      state = State.Pulling;
    }
  }

  public void OnDrag (PointerEventData data) {
    currentDelta = initialY - eventList.transform.position.y;

    if (currentDelta >= pullTrigger) {
      feedView.OnPull();
      AnimateBack();
      Reset();
    }
  }

  public void OnEndDrag (PointerEventData data) {
    if (state != State.Reset) {
      Reset();
    }
  }

  void Reset () {
    state = State.Reset;
    initialY = 0f;
    currentDelta = 0f;
  }

  void AnimateBack () {
    iTween.MoveTo(eventList, iTween.Hash(
      "y", initialY
      ));
  }
}
