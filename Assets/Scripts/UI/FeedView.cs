using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeedView : BaseBehaviour {

  public GameObject pullAnchor;
  public GameObject eventPrefab;
  public int numEvents = 20;
  public Vector2 wordRange = new Vector2(2, 50);

  const string LoremIpsum = @"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
  string[] words;


	// Use this for initialization
	void Start () {
    words = LoremIpsum.Split(new string[]{" "}, System.StringSplitOptions.RemoveEmptyEntries);
	}

  void RandomList () {
    for (int i = 0; i < numEvents; i++) {
      CreateText(i);
    }
  }
	
	// Update is called once per frame
	void Update () {
    if (Input.GetMouseButtonDown(1)) {
      CreateText (transform.childCount);
    }
	}

  public void OnPull () {
    int rand = Random.Range(1, 10);
    for (int i = 0; i < rand; i++) {
      CreateText(transform.childCount);
    }
  }

  string partialString () {
    var r = Random.Range((int)wordRange.x, (int)wordRange.y);
    string[] result = new string[r];
    System.Array.Copy(words, 0, result, 0, r);
    string partial = System.String.Join(" ", result);
    return partial;
  }

  void CreateText (int i) {
    var eventObj = (GameObject)Instantiate (eventPrefab);

    var title = eventObj.transform.FindChild("Title");
    title.GetComponent<Text>().text += ("" + i);
    var desc = eventObj.transform.FindChild("Description");
    desc.GetComponent<Text>().text = partialString();

    eventObj.transform.SetParent(transform, false);
    eventObj.transform.SetAsFirstSibling();
    pullAnchor.transform.SetAsFirstSibling();

  }
}
