using UnityEngine;
using System.Collections;

public class MouseKeyboardCameraBehaviour : MonoBehaviour {

  public Transform cameraTransform;

  Vector3 lastFramePosition;
  public float moveMultiplier = 0.1f;
  public bool reverseX = false;
  public bool reverseZ = false;

  public float zoomMultiplier = 0.1f;
  public bool reverseZoom = false;

  public float defaultZoom = -50f;
  public float minZoom = -10f;
  public float maxZoom = -50f;

	// Use this for initialization
	void Start () {
    cameraTransform = Camera.main.transform;
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
    var pos = cameraTransform.position;
    pos.x += amount.x * (reverseX ? -1 : 1) * moveMultiplier;
    pos.z += amount.y * (reverseZ ? -1 : 1) * moveMultiplier;
    cameraTransform.position = pos;
  }

  void ZoomCamera (float amount) {
    var pos = cameraTransform.position;
    var newY = pos.y + amount * (reverseZoom ? -1 : 1) * zoomMultiplier;
    newY = Mathf.Clamp(newY, minZoom, maxZoom);
    pos.y = newY;
    cameraTransform.position = pos;
  }
}
