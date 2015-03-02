using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttributeReader : BaseBehaviour {

  public enum Attribute {
    Gold,
    Income,
    Expenses,
    CurrentSpeed,
    CurrentTime
  }

  public float updateInterval = 1f;
  public string prefix = "Label: ";
  public Attribute attribute;
  public string suffix = "";

  string AttributeValue {
    get {
      switch(attribute) {
        case Attribute.Gold:
          return sim.Player.Gold.ToString();
        case Attribute.Income:
          return sim.Player.Income.ToString();
        case Attribute.Expenses:
          return sim.Player.Expenses.ToString();
        case Attribute.CurrentSpeed:
          return sim.CurrentSpeed.ToString();
        case Attribute.CurrentTime:
          return sim.GameTime.ToString();
      }
      return "[Not Found]";
    }
  }

  Text text;

	// Use this for initialization
	void Start () {
    text = GetComponent<Text>();
    InvokeRepeating("UpdateValue", 0f, updateInterval);
	}

	// Update is called once per frame
	void Update () {
	}

  void UpdateValue () {
    text.text = string.Format("{0}{1}{2}", prefix, AttributeValue, suffix);
  }
}
