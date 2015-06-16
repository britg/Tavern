using UnityEngine;
using System.Collections;

public class CameraController : BaseBehaviour {

  public Transform cameraTransform;

  public float moveMultiplier = 0.1f;
  public bool reverseX = true;
  public bool reverseZ = true;
  
  public float zoomMultiplier = 1f;
  public bool reverseZoom = true;
  
  public float defaultZoom = 50f;
  public float minZoom = 10f;
  public float maxZoom = 100f;

	// Use this for initialization
	void Start () {
    cameraTransform = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void Move (Vector3 amount) {
    var pos = cameraTransform.position;
    pos.x += amount.x * (reverseX ? -1 : 1) * moveMultiplier;
    pos.z += amount.y * (reverseZ ? -1 : 1) * moveMultiplier;
    cameraTransform.position = pos;
  }

  public void Zoom (float amount) {
    var pos = cameraTransform.position;
    var newY = pos.y + amount * (reverseZoom ? -1 : 1) * zoomMultiplier;
    newY = Mathf.Clamp(newY, minZoom, maxZoom);
    pos.y = newY;
    cameraTransform.position = pos;
  }
}
