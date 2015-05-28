using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeedView : MonoBehaviour {

  public GameObject scrollerPrefab;

	// Use this for initialization
	void Start () {
    GameObject scroller = Instantiate(scrollerPrefab);
    scroller.transform.SetParent(transform, false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
