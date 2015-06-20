using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FeedView : BaseBehaviour {

  public delegate void RefreshFinishedHandler();
  RefreshFinishedHandler refreshFinishedHandler;

  public GameObject pullAnchor;
  public GameObject eventPrefab;
  public GameObject eventInfoPrefab;
  public GameObject eventTransitionPrefab;
  public int numEvents = 20;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

  public void BeginRefresh (RefreshFinishedHandler handler) {
    refreshFinishedHandler = handler;
    pullAnchor.transform.Find("Title").GetComponent<Text>().text = "Questing...";
    // Async here
    Invoke("DoRefresh", 1f);
  }

  void DoRefresh () {
    List<PlayerEvent> newEvents = sim.eventEngine.Input();
//    int rand = Random.Range(1, 10);
//    BatchCreateEvent(rand);

    List<GameObject> eventObjs = new List<GameObject>();
    foreach (var playerEvent in newEvents) {
      eventObjs.Add(CreatePlayerEventView(playerEvent));
    }
    DisplayNewEvents(eventObjs);

    EndRefresh();
  }

  void EndRefresh () {
    pullAnchor.transform.Find("Title").GetComponent<Text>().text = "Pull to Quest";
    refreshFinishedHandler();
  }

  GameObject CreatePlayerEventView (PlayerEvent playerEvent) {
    GameObject prefab = eventPrefab;
    GameObject eventObj;

    if (playerEvent.type == PlayerEvent.Type.Info) {
      prefab = eventInfoPrefab;
    } else if (playerEvent.type == PlayerEvent.Type.Transition) {
      prefab = eventTransitionPrefab;
    } 

    eventObj = (GameObject)Instantiate(prefab);
    eventObj.GetComponent<EventView>().playerEvent = playerEvent;
    return eventObj;
  }

  void DisplayNewEvents (List<GameObject> newEvents) {
    foreach (GameObject eventObj in newEvents) {
      eventObj.transform.SetParent(transform, false);
      eventObj.transform.SetSiblingIndex(1);
    }
  }

}
