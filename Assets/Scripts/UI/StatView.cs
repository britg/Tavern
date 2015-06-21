using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatView : BaseBehaviour {

  public string statKey;

  Stat _stat;
  Stat stat {
    get {
      if (_stat == null) {
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
    text.text = string.Format("{0}", stat.Value);
	}
}
