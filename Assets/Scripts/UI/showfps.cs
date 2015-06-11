using UnityEngine;
using System.Collections;

public class showfps : MonoBehaviour {

	// Use this for initialization
	void Start () {
    Application.targetFrameRate = 60;
	}
	
	// Update is called once per frame
	void Update () {
    Vector3 angles = Camera.main.transform.localEulerAngles;
    angles.y += Time.deltaTime * 5f;
    Camera.main.transform.localEulerAngles = angles;
	}
}
