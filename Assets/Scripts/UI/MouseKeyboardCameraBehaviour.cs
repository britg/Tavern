using UnityEngine;
using System.Collections;

public class MouseKeyboardCameraBehaviour : MonoBehaviour {

  Vector3 lastFramePosition;
  public float moveMultiplier = 0.1f;
  public bool reverseX = false;
  public bool reverseY = false;

  public float zoomMultiplier = 0.1f;
  public bool reverseZoom = false;

  public float minZoom = -10f;
  public float maxZoom = -50f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    if (Input.GetMouseButtonDown(0)) {
      lastFramePosition = Input.mousePosition;
    }

	  if (Input.GetMouseButton(0)) {
      var diff = Input.mousePosition - lastFramePosition;
      MoveCamera(diff);
      lastFramePosition = Input.mousePosition;
    }

    var scrollAmount = Input.GetAxis("Mouse ScrollWheel");
    ZoomCamera(scrollAmount);

	}

  void MoveCamera (Vector3 amount) {
    var pos = transform.position;
    pos.x += amount.x * (reverseX ? -1 : 1) * moveMultiplier;
    pos.y += amount.y * (reverseY ? -1 : 1) * moveMultiplier;
    transform.position = pos;
  }

  void ZoomCamera (float amount) {
    var pos = transform.position;
    var newZ = pos.z + amount * (reverseZoom ? -1 : 1);
    newZ = Mathf.Clamp(newZ, maxZoom, minZoom);
    pos.z = newZ;
    transform.position = pos;
  }
}
