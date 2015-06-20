using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventInfoView : BaseBehaviour {

  public PlayerEvent playerEvent;
  Text description;

	// Use this for initialization
	void Start () {
    description = transform.Find("Description").GetComponent<Text>();
    description.text = playerEvent.Content;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
