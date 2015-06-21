using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatView : BaseBehaviour {

  public string statKey;
  public string prefix = "";
  public string suffix = "";
  public bool includeMax = false;

  Stat _stat;
  Stat stat {
    get {
      if (_stat == null) {
        Debug.Log ("Attempting to load stat " + statKey);
        _stat = sim.player.Stats[statKey];
      }
      return _stat;
    }
  }

  Text _text;
  Text text {
    get {
      if (_text == null) {
        _text = GetComponent<Text>();
      }
      return _text;
    }
  }


  // Use this for initialization
  void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    var val = stat.Value.ToString();
    if (includeMax) {
      val = string.Format("{0}/{1}", stat.Value, stat.MaxValue);
    }
    text.text = string.Format("{0}{1}{2}", prefix, val, suffix);
	}
}
