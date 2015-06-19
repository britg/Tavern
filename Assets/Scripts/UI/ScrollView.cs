using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ScrollView : BaseBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

  enum State {
    Reset,
    Pulling,
    Refreshing
  }

  public GameObject eventList;
  FeedView feedView;
  ScrollRect scrollRect;

  float initialY = 0f;
  float currentDelta = 0f;
  public float pullTrigger = 0.1f;
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
    if (scrollRect.verticalNormalizedPosition >= 1f && state != State.Refreshing) {
      initialY = eventList.transform.position.y;
      state = State.Pulling;
    }
  }

  public void OnDrag (PointerEventData data) {
    currentDelta = initialY - eventList.transform.position.y;
    var currentRatio = currentDelta / Screen.height;

    if (currentRatio >= pullTrigger && state == State.Pulling) {
//      scrollRect.vertical = false;
//      feedView.OnPull();
//      Reset();
      InitiateRefresh();
    }
  }

  public void OnEndDrag (PointerEventData data) {
//    scrollRect.vertical = true;
    if (state == State.Pulling) {
      Reset();
    }
  }

  void Reset () {
    state = State.Reset;
    initialY = 0f;
    currentDelta = 0f;
    scrollRect.vertical = true;
    Invoke("MakeElastic", 0.1f);
  }

  void MakeElastic () {
    scrollRect.movementType = ScrollRect.MovementType.Elastic;
  }

  void InitiateRefresh () {
    Debug.Log ("Initiating refresh...");
    state = State.Refreshing;
    scrollRect.movementType = ScrollRect.MovementType.Clamped;
    scrollRect.vertical = false;
    feedView.BeginRefresh(OnRefreshFinished);
  }

  void OnRefreshFinished () {
    Debug.Log ("Refresh finished");
    Reset();
  }


}
