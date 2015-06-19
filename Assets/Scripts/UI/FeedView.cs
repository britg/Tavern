using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FeedView : BaseBehaviour {

  public delegate void RefreshFinishedHandler();
  RefreshFinishedHandler refreshFinishedHandler;

  public GameObject pullAnchor;
  public GameObject eventPrefab;
  public int numEvents = 20;
  public Vector2 wordRange = new Vector2(2, 50);

  Transform formerTopEvent;
  float formerTopEventInitialY;
  bool isReflowing = false;

  float lastRevolveY = 0f;
  float heightOfLastItem = 0f;

  const string LoremIpsum = @"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
  string[] words;


	// Use this for initialization
	void Start () {
    words = LoremIpsum.Split(new string[]{" "}, System.StringSplitOptions.RemoveEmptyEntries);
    BatchCreateEvent(20);
    var last = transform.GetChild(transform.childCount-1);
    heightOfLastItem =last.GetComponent<RectTransform>().rect.height; 
    lastRevolveY = transform.position.y;
    Debug.Log ("Initial revolve y is " + lastRevolveY);
	}

  void RandomList () {
    for (int i = 0; i < numEvents; i++) {
      CreateText(i);
    }
  }
	
	// Update is called once per frame
	void Update () {
    if (Input.GetMouseButtonDown(1)) {
      BatchCreateEvent(Random.Range (1, 10));
    }

    if (Input.GetMouseButtonDown(0)) {
//      Revolve();
    }

    var offset = lastRevolveY - transform.position.y;
//    Debug.Log ("Offset is " + offset);
    if (offset >= heightOfLastItem) {
//      Revolve();
    }

    if (isReflowing) {
      TopEvent();
    }
	}

  public void BeginRefresh (RefreshFinishedHandler handler) {
    refreshFinishedHandler = handler;
    pullAnchor.transform.Find("Title").GetComponent<Text>().text = "Questing...";
    // Async here
    Invoke("DoRefresh", 3f);
  }

  void DoRefresh () {
    int rand = Random.Range(1, 10);
    BatchCreateEvent(rand);
    EndRefresh();
  }

  void EndRefresh () {
    pullAnchor.transform.Find("Title").GetComponent<Text>().text = "Pull to Quest";
    refreshFinishedHandler();
  }

  void Revolve () {
    var last = transform.GetChild(transform.childCount-1);
    var height = last.GetComponent<RectTransform>().rect.height;
    Debug.Log ("height is " + height);
    var listPos = transform.position;
//    listPos.y += height;
//    transform.position = listPos;
    last.SetSiblingIndex(1);
    lastRevolveY = transform.position.y;
  }

  string partialString () {
    var r = Random.Range((int)wordRange.x, (int)wordRange.y);
    string[] result = new string[r];
    System.Array.Copy(words, 0, result, 0, r);
    string partial = System.String.Join(" ", result);
    return partial;
  }

  void BatchCreateEvent (int count) {
    List<GameObject> eventObjects = new List<GameObject>();
    for (int i = 0; i < count; i++) {
      eventObjects.Add(CreateText(transform.childCount + i));
    }

//    formerTopEvent = transform.GetChild(1);
//    formerTopEventInitialY = formerTopEvent.position.y;

    foreach (GameObject eventObj in eventObjects) {
      eventObj.transform.SetParent(transform, false);
      eventObj.transform.SetSiblingIndex(1);
//      eventObj.SetActive(false);
      if (transform.childCount > 20) {
        var lastEventTrans = transform.GetChild(transform.childCount-1);
        Destroy(lastEventTrans.gameObject);
      }
    }

//    isReflowing = true;
  }

  void TopEvent () {
    if (formerTopEvent == null) {
      return;
    }
    float formerTopEventNewY = formerTopEvent.position.y;
    float diff = formerTopEventInitialY - formerTopEventNewY;

    Debug.Log ("Top event diff is " + diff);
    if (diff > 0f) {
      Vector3 listPos = transform.position;
      listPos.y += diff;
      transform.position = listPos;
      isReflowing = false;
    }
  }


  GameObject CreateText (int i) {
    var eventObj = (GameObject)Instantiate (eventPrefab);

    var title = eventObj.transform.FindChild("Title");
    title.GetComponent<Text>().text += ("" + i);
    var desc = eventObj.transform.FindChild("Description");
    desc.GetComponent<Text>().text = partialString();

    return eventObj;
  }

}
