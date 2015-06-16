using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseKeyboardCameraBehaviour : MonoBehaviour {

  enum SessionType {
    World,
    UI
  }

  public CameraController cameraController;

  public float sessionTravel = 1f;
  SessionType sessionType;

  Vector3 lastFramePosition;
  float sessionDistance = 0f;
  GameObject sessionObject;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    if (Input.GetMouseButtonDown(0)) {
      CaptureMouseDown();
    }

	  if (Input.GetMouseButton(0)) {
      CaptureMouseMove();
    }

    if (Input.GetMouseButtonUp(0)) {
      CaptureMouseUp();
    }

    CaptureScroll();

	}

  void StartSession () {
    sessionDistance = 0;
    lastFramePosition = Input.mousePosition;

    if (EventSystem.current.IsPointerOverGameObject()) {
      sessionType = SessionType.UI;
    } else {
      sessionType = SessionType.World;
    }
  }

  void CaptureMouseDown () {
    StartSession();

    if (sessionType == SessionType.World) {
      CaptureWorldDown();
    }

  }

  void CaptureWorldDown () {

    RaycastHit hit;
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out hit)) {
      sessionObject = hit.collider.gameObject;
      Debug.Log ("Down on " + sessionObject);
    }
  }

  void CaptureMouseMove () {
    if (sessionType == SessionType.UI) {

    } else {
      CaptureWorldMove();
    }
  }

  void CaptureWorldMove () {
    var diff = Input.mousePosition - lastFramePosition;
    sessionDistance += diff.sqrMagnitude;
    cameraController.Move(diff);
    lastFramePosition = Input.mousePosition;
  }

  void CaptureMouseUp () {
    if (sessionType == SessionType.UI) {

    } else {
      CaptureWorldUp();
    }
  }

  void CaptureWorldUp () {
    RaycastHit hit;
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out hit)) {
      var upObject = hit.collider.gameObject;

      if (upObject == sessionObject) {
        if (sessionDistance <= sessionTravel) {
          Debug.Log ("Registering as click " + upObject);
        }
      }
    }
  }

  void CaptureScroll () {
    StartSession();

    if (sessionType == SessionType.World) {
      CaptureGameScroll();
    }
  }

  void CaptureGameScroll () {
    var scrollAmount = Input.GetAxis("Mouse ScrollWheel");
    cameraController.Zoom(scrollAmount);
  }

}
