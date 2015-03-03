using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BuildingList : BaseBehaviour {

  public GameObject labelPrefab;
  public Vector2 offset = new Vector2(0, -20f);
  List<GameObject> labels;

	// Use this for initialization
	void Start () {
    InvokeRepeating("UpdateList", 0f, 1f);
	}

	// Update is called once per frame
	void Update () {

	}

  void UpdateList () {
    ClearList();
    labels = new List<GameObject>();
    foreach (Building bldg in sim.Player.Buildings) {
      GameObject lblObj = (GameObject)Instantiate(labelPrefab);
      lblObj.GetComponent<Text>().text = bldg.Name.ToString();
      lblObj.transform.SetParent(transform, false);
      lblObj.transform.localPosition = offset * labels.Count;
      labels.Add(lblObj);
    }
  }

  void ClearList () {
    if (labels == null) {
      return;
    }
    foreach (GameObject lbl in labels) {
      Destroy(lbl);
    }
  }
}
