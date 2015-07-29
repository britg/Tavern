using UnityEngine;
using System.Collections;

public class ActionView : BaseBehaviour {

  public string actionName;

	public PlayerEvent playerEvent {get; set;}
  public bool hasTriggered = false;
  Rect screenRect;

  // Use this for initialization
  void Start () {
    screenRect = new Rect(0f, 0f, Screen.width + 1, Screen.height + 1);
  }
  
  // Update is called once per frame
  void Update () {
  }

 
}
