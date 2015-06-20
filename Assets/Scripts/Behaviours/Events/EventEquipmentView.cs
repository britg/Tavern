using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventEquipmentView : EventView {

  Text title;

	// Use this for initialization
	void Start () {
    title = transform.Find("Title").GetComponent<Text>();
    var str = string.Format("[{0}]", playerEvent.Content);
    title.text = str;
	}
	
}
